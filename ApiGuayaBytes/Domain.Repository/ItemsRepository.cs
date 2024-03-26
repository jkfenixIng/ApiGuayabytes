using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Dto;


namespace Domain.Repository
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly DataContext Context;
        public ItemsRepository(DataContext context) 
        {
            Context = context;
        }
        public async Task<bool> AddNewItemAsync(Items newItem)
        {
            try
            {
                Context.Items.Add(newItem);
                // Guardar los cambios en la base de datos
                await Context.SaveChangesAsync();
                return true; // Indicar que el usuario se creó exitosamente
            }
            catch (Exception)
            {
                // Manejar excepciones aquí si es necesario
                return false; // Indicar que hubo un error al crear el usuario
            }
        }
        public async Task<bool> UpdateItemAsync(Items item)
        {
            try
            {
                Context.Items.Update(item);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Manejar excepciones aquí si es necesario
                return false; // Indicar que hubo un error al crear el usuario
            }
        }
    }
}