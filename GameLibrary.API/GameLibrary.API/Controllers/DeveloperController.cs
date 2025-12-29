using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopersController(IDeveloperService developerService) : ControllerBase
    {
        private readonly IDeveloperService _developerService = developerService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperDto>>> GetAll()
        {
            var developers = await _developerService.GetAllDevelopersAsync();
            return Ok(developers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperDto>> GetById(int id)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(id);
            if (developer == null)
                return NotFound();

            return Ok(developer);
        }

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<DeveloperDto>> GetByName(string name) {

            var developer = await _developerService.GetDeveloperByNameAsync(name);
            if (developer == null)
                return NotFound();

            return Ok(developer);
        }

        [HttpPost]
        public async Task<ActionResult<DeveloperDto>> Create([FromBody] CreateDeveloperDto dto)
        {
            try
            {
                var developer = await _developerService.CreateDeveloperAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = developer.Id }, developer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _developerService.DeleteDeveloperAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
