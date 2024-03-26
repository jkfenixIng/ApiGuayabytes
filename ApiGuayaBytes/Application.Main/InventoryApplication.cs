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
using Domain.Entities;
using static System.Net.Mime.MediaTypeNames;
using Application.Dto;

namespace Application.Main
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILoginApplication _loginApplication;
        private readonly ILogsRepository _logsRepository;
        public InventoryApplication(IInventoryRepository inventoryRepository, IConfiguration configuration, 
            ILoginApplication loginApplication, ILogsRepository logsRepository)
        {
            _inventoryRepository = inventoryRepository;
            _configuration = configuration;
            _loginApplication = loginApplication;
            _logsRepository = logsRepository;
        }

        public async Task<ResponseDto<bool>> AddUserInventoryAsync(string token, UserInventoryDto newUserInventoryDto)
        {
            var response = new ResponseDto<bool> { Data = false };
            try
            {
                var claims = await _loginApplication.GetClaimsFromTokenAsync(token);
                var newUserInventory = new UserInventory
                {
                    IdUser = int.Parse(claims[1].Value),
                    IdItem = newUserInventoryDto.IdItem,
                    Active = newUserInventoryDto.Active
                };

                bool success = await _inventoryRepository.AddUserInventoryAsync(newUserInventory);

                if (success)
                {
                    await _logsRepository.AddLogAsync(1, string.Format("Se Agrego el item con id {0} al usuario con id {1}", newUserInventoryDto.IdItem, claims[1].Value));
                    // Configurar la respuesta de éxito
                    response.IsSuccess = true;
                    response.Response = "200";
                    response.Message = "Ítem agregado correctamente al inventario.";
                    response.Data = true;
                }
                else
                {            
                    // Configurar la respuesta de error
                    response.IsSuccess = false;
                    response.Response = "400";
                    response.Message = "Error al agregar el ítem al inventario.";
                }

                return response;
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta con el error
                response.IsSuccess = false;
                response.Response = "500";
                response.Message = "Error al agregar el ítem al inventario: " + ex.Message;
                return response;
            }
        }
        public async Task<ResponseDto<List<GetItemsDto>>> GetUserInventoryByCategoryAsync(string token, int categoryId)
        {
            var response = new ResponseDto<List<GetItemsDto>> { Data = new List<GetItemsDto>() };
            try
            {
                var claims = await _loginApplication.GetClaimsFromTokenAsync(token);
                var items = await _inventoryRepository.GetUserInventoryByCategoryAsync(int.Parse(claims[1].Value), categoryId);

                // Mapear los objetos Items a GetItemsDto
                var mappedItems = items.Select(item => new GetItemsDto
                {
                    IdItem = item.IdItem,
                    Name = item.Name,
                    Image = item.Image,
                    Price = item.Price,
                    IdCategory = item.IdCategory
                }).ToList();

                // Configurar la respuesta de éxito
                response.IsSuccess = true;
                response.Response = "200";
                response.Message = "Ítems del inventario obtenidos correctamente.";
                response.Data = mappedItems; 

                return response;
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta con el error
                response.IsSuccess = false;
                response.Response = "500";
                response.Message = "Error al obtener los ítems del inventario por categoría: " + ex.Message;
                return response;
            }
        }
    }
}