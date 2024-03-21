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
    public class UsersAplication : IUsersAplication
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _loginRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogsRepository _logsRepository;
        public UsersAplication(ILoginRepository loginRepository, IConfiguration configuration, IUsersRepository usersRepository, ILogsRepository logsRepository)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
            _usersRepository = usersRepository;
            _logsRepository = logsRepository;
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




    }
}