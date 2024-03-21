using Application.DTO;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application.Main
{
    public class LoginAplication : ILoginAplication
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _loginRepository;
        public LoginAplication(ILoginRepository _LoginRepository, IConfiguration configuration)
        {
            _loginRepository = _LoginRepository;
            _configuration = configuration;
        }


        public async Task<ResponseDto<DataLoginDto>> GetLogin(string NickName, string paswword)
        {
            var data = new ResponseDto<DataLoginDto> { Data = new DataLoginDto() };
            try
            {
                bool existe = await _loginRepository.GetExistUser(NickName);
                if (!existe)
                {
                    data.IsSuccess = existe;
                    data.Response = "400";
                    data.Message = "No existe el usuario.";
                    return data;
                }
                var status = await _loginRepository.GetCoincidenciaPassword(NickName, paswword);
                if (!status)
                {
                    data.IsSuccess = status;
                    data.Response = "400";
                    data.Message = "No coincide la contraseña con el usuario.";
                    return data;
                }
                data.IsSuccess = true;
                data.Response = "200";
                data.Message = "Datos Correctos.";
                data.Data.Token = await GenerateJWT(NickName);
                return data;
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Message = "Error: " + ex.Message;
                data.Response = "500";
                return data;
            }
        }
        private async Task<string> GenerateJWT(string NickName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:IssuerSigningKey"])); // Debe coincidir con la clave de configuración
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, NickName)
        // Aquí puedes agregar más claims según sea necesario
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Token:ValidIssuer"], // Debe coincidir con el emisor de la configuración
                audience: _configuration["Token:ValidAudience"], // Debe coincidir con la audiencia de la configuración
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Tiempo de expiración del token
                signingCredentials: credentials
            );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}