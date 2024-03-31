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
    public class GamesApplication : IGamesApplication
    {
        private readonly ILoginApplication _loginApplication;
        private readonly IGamesRepository _gamesRepository;
        public GamesApplication(IGamesRepository gamesRepository, ILoginApplication loginApplication)
        {
            _gamesRepository = gamesRepository;
            _loginApplication = loginApplication;
        }

        public async Task<ResponseDto<List<GameTypesDto>>> GetAllGameTypesAsync()
        {
            var response = new ResponseDto<List<GameTypesDto>> { Data = new List<GameTypesDto>() };
            try
            {
                var gameTypes = await _gamesRepository.GetAllGameTypesAsync();

                // Mapear los objetos GameTypes a GetGameTypesDto
                var mappedGameTypes = gameTypes.Select(gameType => new GameTypesDto
                {
                    IdGameType = gameType.IdGameType,
                    GameType = gameType.GameType,
                    NumberOfDices = gameType.NumberOfDices,
                    StartingAmount = gameType.StartingAmount,
                    AmountPlayers = gameType.AmountPlayers,
                    MinimumBet = gameType.MinimumBet
                }).ToList();

                // Configurar la respuesta de éxito
                response.IsSuccess = true;
                response.Response = "200";
                response.Message = "Tipos de juego obtenidos correctamente.";
                response.Data = mappedGameTypes; // Asignar los tipos de juego mapeados a Data

                return response;
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta con el error
                response.IsSuccess = false;
                response.Response = "500";
                response.Message = "Error al obtener los tipos de juego: " + ex.Message;
                return response;
            }
        }
  
        public async Task<ResponseDto<int>> CreateNewGameAsync(string token, CreateGameDto createGameDto)
        {
            var response = new ResponseDto<int> { Data = 0 };
            try
            {
                var claims = await _loginApplication.GetClaimsFromTokenAsync(token);
                // Crear un nuevo juego
                var newGame = new Games
                {
                    IdGameType = createGameDto.IdGameType,
                    IdGameMode = createGameDto.IdGameModes
                };

                // Guardar el nuevo juego en la base de datos
                int newGameId = await _gamesRepository.CreateNewGameAsync(newGame);                

                if (newGameId > 0)
                {
                    var newGameHistory = new HistoryGames
                    {
                        IdGame = newGameId,
                        IdUser = int.Parse(claims[1].Value),
                        IsWinner = false
                    };
                    int newGameHistoryId = await _gamesRepository.AddNewHistoryGameAsync(newGameHistory);

                    if (newGameHistoryId > 0)
                    {
                        // Configurar la respuesta de éxito
                        response.IsSuccess = true;
                        response.Response = "200";
                        response.Message = "Nuevo juego creado correctamente.";
                        response.Data = newGameId; // Asignar el ID del juego creado a Data
                    }
                    else
                    {
                        // Configurar la respuesta de error
                        response.IsSuccess = false;
                        response.Response = "400";
                        response.Message = "Error al crear el nuevo historial del juego.";
                    }
                }
                else
                {
                    // Configurar la respuesta de error
                    response.IsSuccess = false;
                    response.Response = "400";
                    response.Message = "Error al crear el nuevo juego.";
                }

                return response;
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta con el error
                response.IsSuccess = false;
                response.Response = "500";
                response.Message = "Error al crear el nuevo juego: " + ex.Message;
                return response;
            }
        }

    }
}