using Application.DTO;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Main
{
    public class LoginAplication : ILoginAplication
    {
        private readonly ILoginRepository _loginRepository;
        public LoginAplication(ILoginRepository _LoginRepository)
        {
            _loginRepository = _LoginRepository;
        }


        public async Task<ResponseDto<DataLogin>> GetLogin(string email, string paswword)
        {
            var data = new ResponseDto<DataLogin> { Data = new DataLogin() };
            try
            {
                bool existe = await _loginRepository.GetExistUser(email);
                if (!existe)
                {
                    data.IsSuccess = existe;
                    data.Response = "400";
                    data.Message = "No existe el usuario.";
                    return data;
                }
                var status = await _loginRepository.GetCoincidenciaPassword(email, paswword);
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
                data.Data.Rol = await _loginRepository.GetRol(email);
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