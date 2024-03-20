using Application.DTO;

namespace Application.Interfaces
{
    public interface ILoginAplication
    {
        Task<ResponseDto<DataLogin>> GetLogin(string email, string paswword);
    }
}