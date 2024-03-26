using Application.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IItemsApplication
    {
        Task<ResponseDto<bool>> AddNewItemAsync(ItemsDto newItemDto);
        Task<ResponseDto<bool>> UpdateItemAsync(int id, ItemsDto itemUpdateDto);
    }
}