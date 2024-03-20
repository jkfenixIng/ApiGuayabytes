namespace Domain.Interfaces
{
    public interface ILoginRepository
    {      
        Task<bool> GetExistUser(string email);
        Task<bool> GetCoincidenciaPassword(string email, string paswword);
        Task<string> GetRol(string email);
    }
}