﻿using ChatApp.Business.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChatApp.Business.Services.UserService.Implementations
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetAuthenticatedUserId()
        {
            var userId = 0;
            if (httpContextAccessor.HttpContext != null)
            {
                var NameIdentifier = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userId = int.Parse(NameIdentifier);
            }
            return userId;
        }

        public string GetAuthenticatedEmail()
        {
            var email = string.Empty;
            if (httpContextAccessor.HttpContext != null)
            {
                email = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            }
            return email;
        }
    }
}
