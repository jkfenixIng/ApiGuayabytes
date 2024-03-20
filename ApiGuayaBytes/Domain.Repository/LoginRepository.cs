using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;


namespace Domain.Repository
{
    public class LoginRepository: ILoginRepository
    {
        private readonly DataContext Context;
        public LoginRepository(DataContext context) 
        {
            Context = context;
        }
        public async Task<bool> GetExistUser(string email)
        {
            return await Context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> GetCoincidenciaPassword(string email, string password)
        {
            return await Context.Users.AnyAsync(u => u.Email == email && u.Password == password);
        }
        public async Task<string> GetRol(string email)
        {
            return await Context.Users
                .Where(u => u.Email == email)
                .Select(u => u.Rol)
                .FirstOrDefaultAsync();
        }
    }
}