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

        private readonly IUsersApplication _usersApplication;

        public UsersController(IUsersApplication usersApplication)
        {
            _usersApplication = usersApplication;
        }

        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] UsersDto usersDto)
        {
            var result = await _usersApplication.CreateNewUser(usersDto);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        [Authorize]
        [HttpGet("GetCashByUserIdAsync")]
        public async Task<IActionResult> GetCashByUserIdAsync()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _usersApplication.GetCashByUserIdAsync(token);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        [Authorize]
        [HttpPatch("UpdateUserCashAsync")]
        public async Task<IActionResult> UpdateUserCashAsync(int newCash)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _usersApplication.UpdateUserCashAsync(token, newCash);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("GetUserNickNameAsync")]
        public async Task<IActionResult> GetUserNickNameAsync()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _usersApplication.GetUserNickNameAsync(token);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        [Authorize]
        [HttpGet("GetAvatarByUserIdAsync")]
        public async Task<IActionResult> GetAvatarByUserIdAsync()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _usersApplication.GetAvatarByUserIdAsync(token);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}