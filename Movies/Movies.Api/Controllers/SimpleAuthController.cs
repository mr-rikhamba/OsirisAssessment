using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Movies.Logic.Models.Configs;
using Microsoft.Extensions.Options;
using Movies.Logic.Models.NytModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    public class SimpleAuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<SimpleAuthController> _logger;
        private readonly AuthKeyModel _authKeyModel;

        public SimpleAuthController(ILogger<SimpleAuthController> logger, IOptions<AuthKeyModel> options)
        {
            _logger = logger;
            _authKeyModel = options.Value;
        }

        /// <summary>
        /// Mock login function that just takes whatever username and does some dummy authentication to generate a token
        /// </summary>
        /// <param name="username">Any username you want to use</param>
        /// <returns>An authentication token used to call movies.</returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                //In real life I would authenticate against database here.
                IdentityUser user = new IdentityUser(loginModel.Username);
                user.Email = $"{loginModel.Username}@osiris.co.za";
                user.Id = Guid.NewGuid().ToString();
                return Ok(GenerateJwtTokens(user));
            }
            return BadRequest("Please enter a valid username.");
        }
        /// <summary>
        /// Generate token from dummy user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private AuthModel GenerateJwtTokens(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authKeyModel.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return new AuthModel { Token = jwtToken};
        }
    }
}
