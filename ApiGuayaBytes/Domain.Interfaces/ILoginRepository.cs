namespace Domain.Interfaces
{
    public interface ILoginRepository
    {      
        Task<bool> GetCoincidenciaPassword(string NickName, string paswword);
        Task<int> GetIdUser(string NickName);
    }
}