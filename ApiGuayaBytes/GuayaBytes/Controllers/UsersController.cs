using Application.Dto;
using Application.Interfaces;
using Application.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuayaBytes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUsersAplication _usersAplication;

        public UsersController(IUsersAplication usersAplication)
        {
            _usersAplication = usersAplication;
        }

        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] UsersDto usersDto)
        {
            var result = await _usersAplication.CreateNewUser(usersDto);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        [Authorize]
        [HttpGet("GetCashByUserIdAsync")]
        public async Task<IActionResult> GetCashByUserIdAsync()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _usersAplication.GetCashByUserIdAsync(token);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        [Authorize]
        [HttpPatch("UpdateUserCashAsync")]
        public async Task<IActionResult> UpdateUserCashAsync(int newCash)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _usersAplication.UpdateUserCashAsync(token, newCash);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}