using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Dto;


namespace Domain.Repository
{
    public class GamesRepository : IGamesRepository
    {
        private readonly DataContext Context;
        public GamesRepository(DataContext context) 
        {
            Context = context;
        }

        public async Task<List<GameTypes>> GetAllGameTypesAsync()
        {
                 return await Context.GameTypes.ToListAsync();
        }

        public async Task<int> CreateNewGameAsync(Games newGame)
        {
            try
            {
                // Agregar el nuevo juego a la base de datos
                Context.Games.Add(newGame);
                await Context.SaveChangesAsync();
                return newGame.IdGame; // Indicar que el juego se creó exitosamente
            }
            catch (Exception)
            {
                // Manejar excepciones aquí si es necesario
                return 0; // Indicar que hubo un error al crear el juego
            }
        }

        public async Task<int> AddNewHistoryGameAsync(HistoryGames historyGame)
        {
            try
            {
                // Agregar el nuevo historial de juego a la base de datos
                Context.HistoryGames.Add(historyGame);
                await Context.SaveChangesAsync();
                return historyGame.IdhistoryGame; // Indicar que el historial de juego se agregó exitosamente
            }
            catch (Exception)
            {
                // Manejar excepciones aquí si es necesario
                return 0; // Indicar que hubo un error al agregar el historial de juego
            }
        }
    }
}