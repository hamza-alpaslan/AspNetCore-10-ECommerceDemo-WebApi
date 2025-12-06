using Application.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Application.Services;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Web.Security;
namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public UserController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            SilUserService userService = new SilUserService();
            var User= userService.UserExistensControl(userLoginDto);
            if (User == null) return BadRequest("User not found");
            var Token = _tokenService.GenerateToken(User);
            return Ok(Token);
        }

    }
}
