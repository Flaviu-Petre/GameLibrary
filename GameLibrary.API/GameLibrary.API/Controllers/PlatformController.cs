using GameLibrary.Service.Dtos.Platform;
using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformController(IPlatformService platformService) : ControllerBase
    {
        private readonly IPlatformService _platformService = platformService;

        [HttpGet]
        public async Task<IActionResult> GetAllPlatforms()
        {
            var platforms = await _platformService.GetAllPlatformsAsync();
            return Ok(platforms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlatformById(int id)
        {
            var platform = await _platformService.GetPlatformByIdAsync(id);
            if (platform == null)
                return NotFound();
            return Ok(platform);
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetPlatformByName(string name)
        {
            var platform = await _platformService.GetPlatformByNameAsync(name);
            if (platform == null)
                return NotFound();
            return Ok(platform);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatform([FromBody] CreatePlatformDto dto)
        {

             var platform = await _platformService.CreatePlatformAsync(dto);
             return CreatedAtAction(nameof(GetPlatformById), new { id = platform.Id }, platform);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlatform(int id, [FromBody] UpdatePlatformDto dto)
        {

             await _platformService.UpdatePlatformAsync(id, dto);
             return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform(int id)
        {
             await _platformService.DeletePlatformAsync(id);
             return NoContent();
        }
    }
}