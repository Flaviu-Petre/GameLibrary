using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Dtos.Publisher;
using GameLibrary.Service.Services;
using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PublishersController(IPublisherService publisherService) : ControllerBase
    {
        private readonly IPublisherService _publisherService = publisherService;
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetAll()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> GetById(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            if (publisher == null)
                return NotFound();
            return Ok(publisher);
        }
        [HttpPost]
        public async Task<ActionResult<PublisherDto>> Create([FromBody] CreatePublisherDto dto)
        {
            var publisher = await _publisherService.CreatePublisherAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = publisher.Id }, publisher);
        }

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<DeveloperDto>> GetByName(string name)
        {

            var developer = await _publisherService.GetPublisherByNameAsync(name);
            if (developer == null)
                return NotFound();

            return Ok(developer);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePublisherDto dto)
        {
            await _publisherService.UpdatePublisherAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _publisherService.DeletePublisherAsync(id);
            return NoContent();
        }
    }
}