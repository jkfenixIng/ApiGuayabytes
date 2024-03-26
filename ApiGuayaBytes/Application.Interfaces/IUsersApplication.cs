using Application.Dto;
using Application.DTO;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Application.Interfaces
{
    public interface IUsersApplication
    {
        Task<ResponseDto<bool>> CreateNewUser(UsersDto usersDto);
        Task<ResponseDto<int?>> GetCashByUserIdAsync(string token);
        Task<ResponseDto<bool>> UpdateUserCashAsync(string token, int? newCash);
        Task<ResponseDto<string>> GetUserNickNameAsync(string token);
        Task<ResponseDto<byte[]?>> GetAvatarByUserIdAsync(string token);
    }
}