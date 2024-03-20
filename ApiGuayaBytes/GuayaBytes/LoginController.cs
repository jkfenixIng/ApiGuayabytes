using Application.Interfaces;
using Application.Main;
using Microsoft.AspNetCore.Mvc;

namespace ApiGuayabytes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ILoginAplication _loginAplication;

        public LoginController(ILogger<LoginController> logger, ILoginAplication loginAplication)
        {
            _logger = logger;
            _loginAplication = loginAplication;
        }

        [HttpGet(Name = "GetLogin")]
        public async Task<IActionResult> GetLogin(string email, string paswword)
        {
            var result = await _loginAplication.GetLogin(email, paswword);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}