using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using prjClassroom.Models;

namespace prjClassroom.Controllers
{
    [ApiController]
    [Route("[usuario]")]
    public class UserLogin : ControllerBase
	{
		public IConfiguration _configuration;
		public UserLogin(IConfiguration configuration)
		{
			_configuration = configuration;
		
		}
		[HttpPost]
		[Route("/login")]
		public dynamic IniciarSesion([FromBody] Object fromData)
		{
			var data = JsonConvert.DeserializeObject<dynamic>(fromData.ToString());

			string username = data.username.ToString();
            string password = data.password.ToString();

			UserEntity user = UserEntity.Db().Where(u => u.Username == username && u.Password == password).FirstOrDefault();

			if(user == null)
			{
				return BadRequest(new
				{
					message= "Bad Credentials"
				});
			}

			var jwt = _configuration.GetSection("Jwt").Get<JwtEntity>();
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
				new Claim("id", user.IdUSer),
                new Claim("username", user.Username),
            };

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
			var signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				jwt.Issuer,
				jwt.Audience,
				claims,
				expires: DateTime.Now.AddMinutes(60),
				signingCredentials: signing
				); 
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
		}
	}
}

