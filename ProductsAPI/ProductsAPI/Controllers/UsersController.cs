using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.DTO;
using ProductsAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase

    {
        private readonly  UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IConfiguration config;

        public UsersController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IConfiguration _config)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            config = _config;
        }

        [HttpPost("register")]
public async Task<IActionResult> CreateUser(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var user = new AppUser
            {
          
                UserName = model.UserName,
                 Email = model.Email,
                FullName = model.FullName,
            
             DateAdded = DateTime.Now

            };

            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                return StatusCode(201);            
                    }


            return BadRequest(result.Errors);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return BadRequest(new { message = "Hatalı email" });

            }
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password,false);
            if (result.Succeeded)
            {
                return Ok(new {token = GenerateJWT(user)});
            }
            return Unauthorized();




        }

        private object GenerateJWT(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Secret").Value ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                }

                ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "yasarapaydin.com"

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }





    }
}
