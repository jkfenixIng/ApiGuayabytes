using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInventoryRepository
    {
        Task<bool> AddUserInventoryAsync(UserInventory userInventory);
        Task<List<Items>> GetUserInventoryByCategoryAsync(int userId, int categoryId);
    }
}