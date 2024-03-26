using Application.DTO;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ILoginApplication
    {
        Task<ResponseDto<DataLoginDto>> GetLogin(string NickName, string paswword);
        Task<ResponseDto<string>> RefreshTokenAsync(HttpContext httpContext);
        Task<List<Claim>> GetClaimsFromTokenAsync(string token);
    }
}