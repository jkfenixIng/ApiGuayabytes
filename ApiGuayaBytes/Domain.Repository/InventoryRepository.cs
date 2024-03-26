using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Dto;


namespace Domain.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DataContext Context;
        public InventoryRepository(DataContext context) 
        {
            Context = context;
        }
        public async Task<bool> AddUserInventoryAsync(UserInventory userInventory)
        {
            try
            {
                Context.UserInventory.Add(userInventory);
                await Context.SaveChangesAsync();
                return true; // Indica que la operación fue exitosa
            }
            catch (Exception)
            {
                return false; // Indica que hubo un error al agregar el registro
            }
        }
        public async Task<List<Items>> GetUserInventoryByCategoryAsync(int userId, int categoryId)
        {
            try
            {
                // Consulta LINQ para obtener los ítems del inventario del usuario por categoría
                var items = await Context.UserInventory
                    .Where(ui => ui.IdUser == userId && ui.Item.IdCategory == categoryId)
                    .Select(ui => ui.Item)
                    .ToListAsync();

                return items;
            }
            catch (Exception)
            {
                // Manejar excepciones aquí si es necesario
                return null;
            }
        }
    }
}