using LittleLive.Core.Models;
using LittleLive.WebApi.Constants;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Helpers
{
    public static class JWTHelper
    {      
        public static string GenerateJWTToken(User userInfo, JWTResource jwtResource)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtResource.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(ClaimConfig.CLAIM_USER_ID, userInfo.Id.ToString()),
                new Claim(ClaimConfig.CLAIM_FULLNAME, userInfo.Name.ToString()),
                new Claim(ClaimConfig.CLAIM_ROLE,userInfo.Role.ToString()),
                new Claim(ClaimConfig.CLAIM_SUBSCRIPTION_TYPE,userInfo.SubscriptionType.ToString()),
                new Claim(ClaimConfig.CLAIM_LICENSE_PLAN,userInfo.LicensePlan.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: jwtResource.Issuer,
                audience: jwtResource.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
