using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Interfaces;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
       public AuthenticationController(IJwtTokenGenerator JwtTokenGenerator)
        {
            _jwtTokenGenerator = JwtTokenGenerator;
        }
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var token= _jwtTokenGenerator.GenerateToken(username, password);
            return Ok(new { Token = token });
        }
    }
}
