using Application.Dto;
using Application.DTO;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Application.Interfaces
{
    public interface IUsersAplication
    {
        Task<ResponseDto<bool>> CreateNewUser(UsersDto usersDto);
        Task<ResponseDto<int?>> GetCashByUserIdAsync(string token);
        Task<ResponseDto<bool>> UpdateUserCashAsync(string token, int? newCash);
    }
}