using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Dtos.Genre;
using GameLibrary.Service.Services;
using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController(IGenreService genreService) : ControllerBase
    {
        private readonly IGenreService _genreService = genreService;

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto payload)
        {
            try
            {
                var result = await _genreService.CreateGenreAsync(payload);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                await _genreService.UpdateGenreAsync(id, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            try
            {
                await _genreService.DeleteGenreByIdAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
