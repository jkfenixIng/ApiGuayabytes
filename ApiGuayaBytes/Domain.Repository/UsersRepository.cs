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
        public async Task<int?> GetCashByUserIdAsync(int userId)
        {
            // Buscar al usuario por su Id y obtener su campo Cash
            var user = await Context.Users.FirstOrDefaultAsync(u => u.IdUser == userId);

            // Si se encontró al usuario, devolver su Cash, de lo contrario, devolver null
            return user?.Cash;
        }
        public async Task<bool> UpdateUserCashAsync(int userId, int? newCashValue)
        {
            // Buscar al usuario por su Id
            var user = await Context.Users.FirstOrDefaultAsync(u => u.IdUser == userId);

            if (user != null)
            {
                // Actualizar el campo Cash
                user.Cash = newCashValue;

                // Guardar los cambios en la base de datos
                await Context.SaveChangesAsync();

                return true; // Indicar que se actualizó el Cash exitosamente
            }
            else
            {
                return false; // Indicar que no se encontró ningún usuario con el Id especificado
            }
        }
    }
}