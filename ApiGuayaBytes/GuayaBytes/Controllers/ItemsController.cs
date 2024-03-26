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
    public class ItemsController : ControllerBase
    {
        private readonly IItemsApplication _itemsApplication;
        public ItemsController(IItemsApplication itemsApplication)
        {
            _itemsApplication = itemsApplication;
        }

        [HttpPost("AddNewItemAsync")]
        public async Task<IActionResult> AddNewItemAsync([FromForm] ItemsDto newItemDto)
        {
            var ok = await _itemsApplication.AddNewItemAsync(newItemDto);
            if (ok.IsSuccess)
            {
                return Ok(ok);
            }
            return BadRequest(ok);
        }
        [HttpPut("UpdateItemAsync")]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromForm] ItemsDto itemUpdateDto)
        {
            var ok = await _itemsApplication.UpdateItemAsync(id, itemUpdateDto);
            if (ok.IsSuccess)
            {
                return Ok(ok);
            }
            return BadRequest(ok);         
        }

    }
}
