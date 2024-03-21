using Application.DTO;

namespace Application.Interfaces
{
    public interface ILoginAplication
    {
        Task<ResponseDto<DataLoginDto>> GetLogin(string NickName, string paswword);
    }
}