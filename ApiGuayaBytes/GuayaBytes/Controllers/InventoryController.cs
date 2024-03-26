using Application.Interfaces;
using Domain.Entities;
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
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryApplication _inventoryApplication;
        public InventoryController(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }
        [Authorize]
        [HttpPost("AddUserInventoryAsync")]
        public async Task<IActionResult> AddUserInventoryAsync([FromBody] UserInventoryDto UserInventoryDto)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var ok = await _inventoryApplication.AddUserInventoryAsync(token,UserInventoryDto);
            if (ok.IsSuccess)
            {
                return Ok(ok);
            }
            return BadRequest(ok);
        }
        [Authorize]
        [HttpGet("GetUserInventoryByCategoryAsync")]
        public async Task<IActionResult> GetUserInventoryByCategoryAsync(int categoryId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var response = await _inventoryApplication.GetUserInventoryByCategoryAsync(token, categoryId);
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
