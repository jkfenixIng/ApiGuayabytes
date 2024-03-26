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
using Application.Dto;

namespace Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _loginRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogsRepository _logsRepository;
        private readonly ILoginApplication _loginApplication;
       public UsersApplication(ILoginRepository loginRepository, IConfiguration configuration, 
           IUsersRepository usersRepository, ILogsRepository logsRepository, ILoginApplication loginApplication)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
            _usersRepository = usersRepository;
            _logsRepository = logsRepository;
            _loginApplication = loginApplication;
        }
        public async Task<ResponseDto<bool>> CreateNewUser(UsersDto usersDto)
        {
            var data = new ResponseDto<bool> { Data = false };
            try
            {
                bool existe = await _usersRepository.GetExistUser(usersDto.NickName);
                if (existe)
                {
                    data.IsSuccess = !existe;
                    data.Response = "400";
                    data.Message = string.Format("Usuario {0} ya creado, no se agregaron los datos.", usersDto.NickName);
                    return data;
                }
                existe = await _usersRepository.EmailExistsAsync(usersDto.Email);
                if (existe)
                {
                    data.IsSuccess = !existe;
                    data.Response = "400";
                    data.Message = string.Format("Correo {0} ya utilizado, no se agregraron los datos.", usersDto.Email);
                    return data;
                }
                var status = await _usersRepository.CreateUserAsync(usersDto);
                if (!status)
                {
                    data.IsSuccess = status;
                    data.Response = "400";
                    data.Message = "Problema al agregar usuario intentelo mas tarde.";
                    return data;
                }
                await _logsRepository.AddLogAsync(1,string.Format("Se Agrego el nuevo usuario {0}", usersDto.NickName));
                data.IsSuccess = true;
                data.Response = "200";
                data.Message = "Usuario Agregado.";
                data.Data = status;
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

        public async Task<ResponseDto<int?>> GetCashByUserIdAsync(string token)
        {
            var data = new ResponseDto<int?> { Data = 0 };
            try
            {
                var claims = await  _loginApplication.GetClaimsFromTokenAsync(token);
                var cash = await _usersRepository.GetCashByUserIdAsync(int.Parse(claims[1].Value));
                if (!cash.HasValue)
                {
                    data.IsSuccess = false;
                    data.Response = "400";
                    data.Message = "Problema al cargar el Dinero del usuario.";
                    return data;
                }
                data.IsSuccess = true;
                data.Response = "200";
                data.Message = "Dinero.";
                data.Data = cash;
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
        public async Task<ResponseDto<bool>> UpdateUserCashAsync(string token, int? newCash)
        {
            var data = new ResponseDto<bool> { Data = false };
            try
            {
                var claims = await _loginApplication.GetClaimsFromTokenAsync(token);
                var cash = await _usersRepository.GetCashByUserIdAsync(int.Parse(claims[1].Value));
                var status = await _usersRepository.UpdateUserCashAsync(int.Parse(claims[1].Value), (cash + newCash));
                if (!status)
                {
                    data.IsSuccess = status;
                    data.Response = "400";
                    data.Message = "Problema al actualizar el dinero intentelo mas tarde.";
                    return data;
                }
                await _logsRepository.AddLogAsync(1, string.Format("Se actualizo el dinero al usuario {0}", claims[0].Value));
                data.IsSuccess = true;
                data.Response = "200";
                data.Message = "Dinero Actualizado.";
                data.Data = status;
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
        public async Task<ResponseDto<string>> GetUserNickNameAsync(string token)
        {
            var data = new ResponseDto<string> { Data = string.Empty };
            try
            {
                var claims = await _loginApplication.GetClaimsFromTokenAsync(token);
                if (!claims.Any())
                {
                    data.IsSuccess = false;
                    data.Response = "400";
                    data.Message = "Problema al consultar nombre del usuario intentelo mas tarde.";
                    return data;
                }
                data.IsSuccess = true;
                data.Response = "200";
                data.Message = "Nombre usuario.";
                data.Data = claims[0].Value;
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
        public async Task<ResponseDto<byte[]?>> GetAvatarByUserIdAsync(string token)
        {
            var data = new ResponseDto<byte[]?> { Data = null };
            try
            {
                var claims = await _loginApplication.GetClaimsFromTokenAsync(token);
                var avatar = await _usersRepository.GetAvatarByUserIdAsync(int.Parse(claims[1].Value));
                if (!avatar.Any())
                {
                    data.IsSuccess = false;
                    data.Response = "400";
                    data.Message = "Problema al consultar avatar del usuario intentelo mas tarde.";
                    return data;
                }
                data.IsSuccess = true;
                data.Response = "200";
                data.Message = "Avatar usuario.";
                data.Data = avatar;
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