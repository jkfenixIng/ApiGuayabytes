using Application.Dto;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<bool> GetExistUser(string NickName);
        Task<bool> CreateUserAsync(UsersDto userDto);
        Task<bool> EmailExistsAsync(string email);
        Task<int?> GetCashByUserIdAsync(int userId);
        Task<bool> UpdateUserCashAsync(int userId, int? newCashValue);
        Task<byte[]?> GetAvatarByUserIdAsync(int userId);
    }
}