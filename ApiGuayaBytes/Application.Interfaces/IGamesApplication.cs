using Application.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IGamesApplication
    {
        Task<ResponseDto<List<GameTypesDto>>> GetAllGameTypesAsync();
        Task<ResponseDto<int>> CreateNewGameAsync(string token, CreateGameDto createGameDto);
    }
}