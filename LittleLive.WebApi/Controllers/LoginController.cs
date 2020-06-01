using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LittleLive.Core.Models;
using LittleLive.Core.Service;
using LittleLive.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LittleLive.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
       
        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginCredential login)
        {
            IActionResult response = Unauthorized();
            var user = await _userService.AuthenticateUser(login);
            if (user != null)
            {
                JWTResource jwtResource = new JWTResource
                {
                    SecretKey = _config["Jwt:SecretKey"],
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Audience"]
                };

                var tokenString = JWTHelper.GenerateJWTToken(user, jwtResource);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
    }
}
