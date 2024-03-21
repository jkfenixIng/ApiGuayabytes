using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Dto;


namespace Domain.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext Context;
        public UsersRepository(DataContext context) 
        {
            Context = context;
        }
        public async Task<bool> GetExistUser(string NickName)
        {
            return await Context.Users.AnyAsync(u => u.NickName.ToLower() == NickName.ToLower());
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await Context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> CreateUserAsync(UsersDto userDto)
        {
            try
            {
                var newUser = new Users
                {
                    Name = userDto.Name,
                    NickName = userDto.NickName,
                    Email = userDto.Email,
                    Password = userDto.Password,
                };

                Context.Users.Add(newUser);
                await Context.SaveChangesAsync();

                return true; // Indicar que el usuario se creó exitosamente
            }
            catch (Exception)
            {
                // Manejar excepciones aquí si es necesario
                return false; // Indicar que hubo un error al crear el usuario
            }
        }
    }
}