using Application.DTO;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

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
        public async Task<ResponseDto<string>> RefreshTokenAsync(HttpContext httpContext)
        {
            var data = new ResponseDto<string> { Data = "" };
            try
            {
                // Obtener el token del encabezado de autorización
                var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                // Leer el token JWT para obtener su contenido
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

                // Obtener la clave secreta de la configuración
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:IssuerSigningKey"]));

                // Configurar los parámetros de validación del token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Token:ValidIssuer"],
                    ValidAudience = _configuration["Token:ValidAudience"],
                    IssuerSigningKey = securityKey
                };

                // Verificar y leer el token
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                var tokenn = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // Obtener la fecha de expiración actual del token
                var expiration = tokenn.ValidTo;

                // Calcular la diferencia entre la fecha de expiración actual y la fecha actual
                var timeUntilExpiration = expiration.Subtract(DateTime.UtcNow);

                // Extender la vida útil del token si faltan 1 minuto o menos para que expire
                // o si el token ha expirado pero está dentro de un rango de 2 minutos de la fecha de expiración.
                if (timeUntilExpiration.TotalMinutes <= 1 && timeUntilExpiration.TotalMinutes >= -2)
                {
                    var newExpiration = DateTime.UtcNow.AddMinutes(30);

                    var newToken = new JwtSecurityToken(
                        jwtToken.Issuer,
                        jwtToken.Audiences.FirstOrDefault(),
                        jwtToken.Claims,
                        DateTime.UtcNow,
                        newExpiration,
                        jwtToken.SigningCredentials
                    );

                    // Genera el nuevo token JWT
                    var newTokenString = tokenHandler.WriteToken(newToken);

                    data.IsSuccess = true;
                    data.Message = "Token nuevo";
                    data.Response = "200";
                    data.Data = newTokenString;
                }
                else
                {
                    data.IsSuccess = true;
                    data.Message = "Token igual";
                    data.Response = "200";
                    data.Data = token;
                }

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
    }
}