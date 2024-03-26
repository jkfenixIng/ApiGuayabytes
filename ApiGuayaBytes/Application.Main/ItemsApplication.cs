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

namespace Application.Main
{
    public class ItemsApplication : IItemsApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IItemsRepository _itemsRepository;
        public ItemsApplication(IItemsRepository itemsRepository, IConfiguration configuration)
        {
            _itemsRepository = itemsRepository;
            _configuration = configuration;
        }

        public async Task<ResponseDto<bool>> AddNewItemAsync(ItemsDto newItemDto)
        {
            var response = new ResponseDto<bool> { Data = false };
            try
            {
                // Crear un nuevo Item a partir del DTO
                var newItem = new Items
                {
                    Name = newItemDto.Name,
                    Image = await ImageToByteAsync(newItemDto.Image),
                    Price = newItemDto.Price,
                    IdCategory = newItemDto.IdCategory
                };

                bool success = await _itemsRepository.AddNewItemAsync(newItem);

                if (success)
                {
                    // Configurar la respuesta de éxito
                    response.IsSuccess = true;
                    response.Response = "200";
                    response.Message = "Ítem agregado correctamente.";
                    response.Data = true;
                }
                else
                {
                    // Configurar la respuesta de error
                    response.IsSuccess = false;
                    response.Response = "400";
                    response.Message = "Error al agregar el ítem.";
                }

                return response;
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta con el error
                response.IsSuccess = false;
                response.Response = "500";
                response.Message = "Error al agregar el ítem: " + ex.Message;
                return response;
            }
        }
        public async Task<ResponseDto<bool>> UpdateItemAsync(int id, ItemsDto itemUpdateDto)
        {
            var response = new ResponseDto<bool> { Data = false };
            try
            {
                // Crear un nuevo Item a partir del DTO
                var item = new Items
                {
                    IdItem = id,
                    Name = itemUpdateDto.Name,
                    Image = await ImageToByteAsync(itemUpdateDto.Image),
                    Price = itemUpdateDto.Price,
                    IdCategory = itemUpdateDto.IdCategory
                };

                bool success = await _itemsRepository.UpdateItemAsync(item);

                if (success)
                {
                    // Configurar la respuesta de éxito
                    response.IsSuccess = true;
                    response.Response = "200";
                    response.Message = "Ítem actualizado  correctamente.";
                    response.Data = true;
                }
                else
                {
                    // Configurar la respuesta de error
                    response.IsSuccess = false;
                    response.Response = "400";
                    response.Message = "Error al agregar el ítem.";
                }

                return response;
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta con el error
                response.IsSuccess = false;
                response.Response = "500";
                response.Message = "Error al agregar el ítem: " + ex.Message;
                return response;
            }
        }
        private async Task<byte[]> ImageToByteAsync(IFormFile Image)
        {
            using (var ms = new MemoryStream())
            {
                await Image.CopyToAsync(ms);
                return ms.ToArray();

            }
        }
    }
}