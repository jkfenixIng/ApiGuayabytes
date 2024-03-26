using Application.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IInventoryApplication
    {
        Task<ResponseDto<bool>> AddUserInventoryAsync(string token, UserInventoryDto newUserInventoryDto);
        Task<ResponseDto<List<GetItemsDto>>> GetUserInventoryByCategoryAsync(string token, int categoryId);
    }
}