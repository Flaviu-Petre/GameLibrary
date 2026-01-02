using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamerPowerController(IGamerPowerService gamerPowerService)
    {
        private readonly IGamerPowerService _gamerPowerService = gamerPowerService;

        [HttpGet("giveaways/{pageNumber}")]
        public async Task<IActionResult> GetGiveaways(int pageNumber)
        {
            var result = await 
        }
    }
}
