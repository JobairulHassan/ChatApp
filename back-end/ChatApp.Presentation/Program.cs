using ChatApp.Business.Hubs;
using ChatApp.Business.Services.PrivateMessageServices.Implementations;
using ChatApp.Business.Services.PrivateMessageServices.Interfaces;
using ChatApp.Business.Services.UserService.Implementations;
using ChatApp.Business.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ChatApp.Persistence.DbContexts;
using ChatApp.Persistence.Repositories.Implementations;
using ChatApp.Persistence.Repositories.Interfaces;
using ChatApp.Presentation;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//UserServices
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddScoped<IUserRetrievalService, UserRetrievalService>();

//MessagesServices
builder.Services.AddScoped<IPrivateMessageService, PrivateMessageService>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPrivateMessageRepository, PrivateMessageRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Retrieve the TokenKey from configuration
        var tokenKey = builder.Configuration.GetSection("AppSettings:TokenKey").Value;

        // Check if TokenKey is null or empty
        if (string.IsNullOrEmpty(tokenKey))
        {
            throw new InvalidOperationException("TokenKey is not configured in AppSettings.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(options =>
    options.WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
