using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GuayaBytes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginAplication _loginAplication;
        public AuthController(ILoginAplication loginAplication)
        {
            _loginAplication = loginAplication;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string NickName, string password)
        {
            var ok = await _loginAplication.GetLogin(NickName, password);
            if (ok.IsSuccess) // Aquí deberías hacer la autenticación real
            {
                return Ok(ok);
            }
            return Unauthorized(ok);
        }

        [HttpGet("refresh")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var updatedToken = await _loginAplication.RefreshTokenAsync(HttpContext);
            if(updatedToken.IsSuccess) 
                return Ok(new { token = updatedToken });
            else
                return BadRequest(updatedToken);
        }
    }
}
