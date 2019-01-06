using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DATINGAPP.API.Data;
using DATINGAPP.API.Dtos;
using DATINGAPP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DATINGAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo,IConfiguration config)
        {
         _repo = repo;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDtos userRegDtos){

        userRegDtos.username = userRegDtos.username.ToLower();
           if (await _repo.UserExist(userRegDtos.username))
                return BadRequest("User already Exist...");

            var CreatedUser=new User
            {
               Username=userRegDtos.username
            };

            var RegisteredUser = await _repo.Register(CreatedUser,userRegDtos.password);

            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDtos userlogin){

                var userFormRepo = await _repo.Login(userlogin.username.ToLower(),userlogin.password);
                if (userFormRepo == null)
                 return Unauthorized();


                var claims = new[]{
                    new Claim(ClaimTypes.NameIdentifier,userFormRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name,userFormRepo.Username)
                };

                var keys= new SymmetricSecurityKey (Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds= new SigningCredentials( keys,SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                 {
                     Subject = new ClaimsIdentity(claims),
                     Expires = DateTime.Now.AddDays(1),
                     SigningCredentials= creds
                 };
                 var tokenHandler = new JwtSecurityTokenHandler();
                 var token = tokenHandler.CreateToken(tokenDescriptor);

                 return Ok(new {
                     token = tokenHandler.WriteToken(token)
                 });

        }

    }
}