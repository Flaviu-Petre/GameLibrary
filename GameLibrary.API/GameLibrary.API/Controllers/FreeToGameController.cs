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
            var result = await _freeToGameService.GetFreeGamesAsync();
            return Ok(result);
        }
    }
}
