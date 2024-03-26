using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IItemsRepository
    {
        Task<bool> AddNewItemAsync(Items newItem); 
        Task<bool> UpdateItemAsync(Items item);
    }
}