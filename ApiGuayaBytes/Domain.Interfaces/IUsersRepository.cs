using Application.Dto;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<bool> GetExistUser(string NickName);
        Task<bool> CreateUserAsync(UsersDto userDto);
        Task<bool> EmailExistsAsync(string email);
    }
}