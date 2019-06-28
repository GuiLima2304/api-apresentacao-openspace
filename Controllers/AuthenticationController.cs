using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenSpace.Model;

namespace OpenSpace.Controllers{

    [AllowAnonymous]
    [ApiController]
    [Route("api/autenticacao")]

    public class AuthenticationController:Controller{

        private readonly IConfiguration configuration;

        public AuthenticationController(IConfiguration configuration){

            this.configuration = configuration;
        }

            ///<summary>
            ///Login, gera um token
            ///</summary>
            ///<result></result>

        [HttpPost]

        public ActionResult Post(Login login){
            
            string token = CriarToken(login);

            return Ok(token);
        }

        private string CriarToken(Login login){

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["TokenConfigurations:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = configuration["TokenConfigurations:Issuer"],
                Audience = configuration["TokenConfigurations:Audience"],
                Subject = new ClaimsIdentity(new GenericIdentity(login.Usuario, "user"))
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

    }
}