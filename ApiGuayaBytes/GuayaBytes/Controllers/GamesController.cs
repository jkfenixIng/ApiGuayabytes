using Application.Dto;
using Application.Interfaces;
using Application.Main;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuayaBytes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {

        private readonly IGamesApplication _gamesApplication;

        public GamesController(IGamesApplication gamesApplication)
        {
            _gamesApplication = gamesApplication;
        }

        [HttpGet("GetAllGameTypesAsync")]
        public async Task<IActionResult> GetAllGameTypesAsync()
        {
            try
            {
                var response = await _gamesApplication.GetAllGameTypesAsync();
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPost("CreateNewGameAsync")]
        public async Task<IActionResult> CreateNewGameAsync([FromBody] CreateGameDto createGameDto)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var response = await _gamesApplication.CreateNewGameAsync(token,createGameDto);
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}