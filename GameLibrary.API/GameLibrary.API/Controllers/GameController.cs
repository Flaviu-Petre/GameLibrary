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

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetGameById(int id)
        //{
        //    var result = await _gameService.GetGameByIdAsync(id);
        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}

        //[HttpGet("getByName/{name}")]
        //public async Task<IActionResult> GetGameByName(string name)
        //{
        //    var result = await _gameService.GetGameByNameAsync(name);
        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateGameById(int id, [FromBody] UpdateGameDto dto)
        //{
        //    try
        //    {
        //        await _gameService.UpdateGameAsync(id, dto);
        //        return NoContent();
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGameById(int id)
        //{
        //    try
        //    {
        //        await _gameService.DeleteGameByIdAsync(id);
        //        return NoContent();
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
    }
}
