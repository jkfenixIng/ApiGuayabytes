using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGamesRepository
    {
        Task<List<GameTypes>> GetAllGameTypesAsync();
        Task<int> CreateNewGameAsync(Games newGame);
        Task<int> AddNewHistoryGameAsync(HistoryGames historyGame);
    }
}