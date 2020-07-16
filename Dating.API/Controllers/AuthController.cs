using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dating.API.Data;
using Dating.API.Dtos;
using Dating.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Dating.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDtos userRegisterDtos)
        {
            // if(!ModelState.IsValid)
            // return BadRequest(ModelState);
            //VAlidate request
            userRegisterDtos.Username = userRegisterDtos.Username.ToLower();

            if (await _repo.UserExists(userRegisterDtos.Username))
                return BadRequest("User Exists");

            // return (IAuthRepository)BadRequest();

            var userToCreate = new User
            {
                Username = userRegisterDtos.Username
            };

            var createdUser = await _repo.Register(userToCreate, userRegisterDtos.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserLoginDtos userLoginDtos)
        {
            var userFromRepo = await _repo.Login(userLoginDtos.Username.ToLower(), userLoginDtos.Password);
            
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
            token = tokenHandler.WriteToken(token)
            });
        //     public async Task<IActionResult> Login(UserLoginDtos userForLoginDto)
        // {
        //     var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

        //     if (userFromRepo == null)
        //         return Unauthorized();

        //     // generate token
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new Claim[]
        //         {
        //             new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString())//,
        //             //new Claim(ClaimTypes.Name, userFromRepo.Username)
        //         }),
        //         Expires = DateTime.Now.AddDays(1),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //         SecurityAlgorithms.HmacSha512Signature)
        //     };

        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     var tokenString = tokenHandler.WriteToken(token);

        //     return Ok(new { tokenString });

        }
            

    }


}
