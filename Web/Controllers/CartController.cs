using Application.DTOs.Cart;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User,Admin")]//[Authorize] yapılabilir ama o zaman giriş yapan herhangi bir kişi
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;

        public CartController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public Task<CartGetDto> TestCartMapping()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var dd= identity.Claims.FirstOrDefault(x=> x.Type==ClaimTypes.NameIdentifier);//Kullanıcı bilgilerine erişme
            SilCartService _silCartService = new SilCartService(_mapper);
            return _silCartService.GetCart("5");
        }
    }
}
