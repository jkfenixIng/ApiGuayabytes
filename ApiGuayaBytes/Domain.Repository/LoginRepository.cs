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
        public async Task<bool> GetCoincidenciaPassword(string NickName, string password)
        {
            return await Context.Users.AnyAsync(u => u.NickName.ToLower() == NickName.ToLower() && u.Password == password);
        }
        public async Task<int> GetIdUser(string NickName)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.NickName == NickName);
            return user != null ? user.IdUser : -1;
        }
    }
}