namespace Domain.Interfaces
{
    public interface ILoginRepository
    {      
        Task<bool> GetExistUser(string NickName);
        Task<bool> GetCoincidenciaPassword(string NickName, string paswword);
    }
}