﻿using Microsoft.IdentityModel.Tokens;
using ChatApp.Persistence.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatApp.Business.Util
{
    public static class TokenGenerator
    {
        public static string Generate(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                 Configration.config.GetSection("AppSettings:TokenKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}