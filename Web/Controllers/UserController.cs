using Application.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Model;
using Application.Services;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        public UserController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            SilUserService userService = new SilUserService();
            var User= userService.UserExistensControl(userLoginDto);
            if (User == null) return BadRequest("User not found");
            var Token = CreateToken(User);
            return Ok(Token);
        }
        private string CreateToken(ApplicationUser applicationUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimArry = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, applicationUser.Name),
                new Claim(ClaimTypes.Role, applicationUser.Role)
            };
            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimArry,
                expires:DateTime.Now.AddDays(7),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
