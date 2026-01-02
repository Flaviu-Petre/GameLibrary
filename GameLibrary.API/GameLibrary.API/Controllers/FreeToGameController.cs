using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreeToGameController(IFreeToGameService freeToGameService) : ControllerBase
    {
        private readonly IFreeToGameService _freeToGameService = freeToGameService;

        [HttpGet("free-games")]
        public async Task<IActionResult> GetFreeGames()
        {
            try
            {
                var result = await _freeToGameService.GetFreeGamesAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, $"Error fetching from FreeToGame API: {ex.Message}");
            }
        }
    }
}
