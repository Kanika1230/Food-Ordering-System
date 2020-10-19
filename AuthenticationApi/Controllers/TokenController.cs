using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthenticationApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationApi.Controllers
{
    
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly Ordering_FoodContext context;
        readonly log4net.ILog _log4net;

        public TokenController( Ordering_FoodContext _context)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(TokenController));
       
            context = _context;
        }
       
        [HttpPost]
        [Route("/api/Token/LoginDetail")]
        public IActionResult LoginDetail([FromBody]Login model)
        {
          
            IActionResult response = Unauthorized();
            var existUser = context.Login.Where(u => u.UserId == model.UserId).FirstOrDefault();
            if(existUser!=null)
            {
                if (existUser.UserId == model.UserId && existUser.Password == model.Password)
                {
                    return Ok(new { token = GenerateJSONWebToken(existUser) });
                }

            }
            _log4net.Info(" User Unauthorised");
            return response;
        }
        string GenerateJSONWebToken(Login model)
        {
            string key = "ProjectSecretKey";
            var issuer = "StudentManagementSystem";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer,
                issuer, null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}