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

        [HttpGet("getByCountry/{country}")]
        public async Task<ActionResult<IEnumerable<DeveloperDto>>> SP_GetByCountry(string country)
        {
            try
            {
                var developers = await _developerService.SP_GetDevelopersByCountryAsync(country);
                return Ok(developers);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDeveloperDto dto)
        {
            try
            {
                await _developerService.UpdateDeveloperAsync(id, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
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

        [HttpGet("sp/paginated")]
        public async Task<ActionResult<IEnumerable<DeveloperDto>>> SP_GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var developers = await _developerService.SP_GetDevelopersPaginatedAsync(pageNumber, pageSize);
            return Ok(developers);
        }
    }
}
