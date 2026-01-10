using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Dtos.Genre;
using GameLibrary.Service.Services;
using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController(IGenreService genreService) : ControllerBase
    {
        private readonly IGenreService _genreService = genreService;

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto payload)
        {
             var result = await _genreService.CreateGenreAsync(payload);
             return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var result = await _genreService.GetAllGenresAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var result = await _genreService.GetGenreByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetGenreByName(string name)
        {
            var result = await _genreService.GetGenreByNameAsync(name);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenreById(int id, [FromBody] UpdateGenreDto dto)
        {
            await _genreService.UpdateGenreAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            await _genreService.DeleteGenreByIdAsync(id);
            return NoContent();
        }

        [HttpGet("sp/search")]
        public async Task<ActionResult<IEnumerable<SearchGenreDto>>> SP_SearchByPartialName([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term cannot be empty.");

            var result = await _genreService.SP_GetGenresByPartialNameAsync(term);
            return Ok(result);
        }

        [HttpGet("sp/paginated")]
        public async Task<ActionResult<IEnumerable<SearchGenreDto>>> SP_GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _genreService.SP_GetGenresPaginatedAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}
