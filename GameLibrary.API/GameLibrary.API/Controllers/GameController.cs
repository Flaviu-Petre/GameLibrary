using GameLibrary.Integration.Exceptions;
using GameLibrary.Service.Dtos.Game;
using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameDto payload)
        {
            try
            {
                var result = await _gameService.CreateGameAsync(payload);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var result = await _gameService.GetAllGamesAsync();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameById(int id)
        {
            try
            {
                await _gameService.DeleteGameByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex) // Aceasta vine din BaseRepository
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex) // Aceasta vine din validarea Service-ului
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameById(int id, [FromBody] UpdateGameDto dto)
        {
            try
            {
                await _gameService.UpdateGameAsync(id, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the game.");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            var result = await _gameService.GetGameByIdAsync(id);

            if (result == null)
                return NotFound($"Game with ID {id} not found.");

            return Ok(result);
        }
        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetGameByName(string name)
        {
            try
            {
                var result = await _gameService.GetGameByNameAsync(name);

                if (result == null)
                    return NotFound($"Game with title '{name}' not found.");

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
