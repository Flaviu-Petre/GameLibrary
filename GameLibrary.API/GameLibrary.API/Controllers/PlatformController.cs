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

        [HttpGet("sp/paginated")]
        public async Task<ActionResult<IEnumerable<PlatformDto>>> SP_GetPaginated(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var platforms = await _platformService.SP_GetPlatformsPaginatedAsync(pageNumber, pageSize);
            return Ok(platforms);
        }

        [HttpGet("sp/search")]
        public async Task<ActionResult<IEnumerable<PlatformDto>>> SP_SearchByName([FromQuery] string term)
        {
            var platforms = await _platformService.SP_SearchPlatformsByNameAsync(term);
            return Ok(platforms);
        }

        [HttpGet("sp/byYear")]
        public async Task<ActionResult<IEnumerable<PlatformDto>>> SP_GetByReleaseYear([FromQuery] int year)
        {
            var platforms = await _platformService.SP_GetPlatformsByReleaseYearAsync(year);
            return Ok(platforms);
        }

    }
}