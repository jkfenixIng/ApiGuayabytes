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
        public async Task<bool> GetExistUser(string NickName)
        {
            return await Context.Users.AnyAsync(u => u.NickName == NickName);
        }
        public async Task<bool> GetCoincidenciaPassword(string NickName, string password)
        {
            return await Context.Users.AnyAsync(u => u.NickName == NickName && u.Password == password);
        }
    }
}