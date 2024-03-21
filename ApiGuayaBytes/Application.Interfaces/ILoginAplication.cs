using Application.DTO;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Application.Interfaces
{
    public interface ILoginAplication
    {
        Task<ResponseDto<DataLoginDto>> GetLogin(string NickName, string paswword);
        Task<ResponseDto<string>> RefreshTokenAsync(HttpContext httpContext);
    }
}