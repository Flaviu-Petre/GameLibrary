using GameLibrary.API.Controllers;
using GameLibrary.Service.Dtos.Genre;
using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameLibrary.Test.Api
{
    public class GenreControllerTests
    {
        private readonly Mock<IGenreService> _mockService;
        private readonly GenreController _controller;

        public GenreControllerTests()
        {
            _mockService = new Mock<IGenreService>();
            _controller = new GenreController(_mockService.Object);
        }

        [Fact]
        public async Task GetGenreById_ReturnsOk_WhenGenreExists()
        {
            // arrange
            var testId = 1;
            var genreDto = new GenreDto { Id = testId, Name = "RPG" };
            _mockService.Setup(s => s.GetGenreByIdAsync(testId)).ReturnsAsync(genreDto);

            // act
            var result = await _controller.GetGenreById(testId);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedGenre = Assert.IsType<GenreDto>(okResult.Value);
            Assert.Equal("RPG", returnedGenre.Name);
        }

        [Fact]
        public async Task CreateGenre_ReturnsOk_WithCreateGenre()
        {
            // arrange
            var createDto = new CreateGenreDto { Name = "Strategy" };
            var resultDto = new GenreDto { Id = 10, Name = "Strategy" };
            _mockService.Setup(s => s.CreateGenreAsync(createDto)).ReturnsAsync(resultDto);

            // act
            var result = await _controller.CreateGenre(createDto);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(resultDto, okResult.Value);
        }

        [Fact]
        public async Task GetGenreById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // arrange
            _mockService.Setup(s => s.GetGenreByIdAsync(999)).ReturnsAsync((GenreDto)null);

            // act
            var result = await _controller.GetGenreById(999);

            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetGenreByName_ReturnsOk_WhenNameExists()
        {
            // arrange
            var genreDto = new GenreDto { Name = "RPG", Description = "Role playing" };
            _mockService.Setup(s => s.GetGenreByNameAsync("RPG")).ReturnsAsync(genreDto);

            // act
            var result = await _controller.GetGenreByName("RPG");

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetGenreByName_ReturnsNotFound_WhenNameDoesNotExist()
        {
            // arrange
            _mockService.Setup(s => s.GetGenreByNameAsync("Unknown")).ReturnsAsync((GenreDto)null);

            // act
            var result = await _controller.GetGenreByName("Unknown");

            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllGenres_ReturnsOk_EvenIfEmpty()
        {
            // arrange
            _mockService.Setup(s => s.GetAllGenresAsync()).ReturnsAsync(new List<GenreDto>());

            // act
            var result = await _controller.GetAllGenres();

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateGenreById_ReturnsNoContent_WhenSuccessful()
        {
            // arrange
            var updateDto = new UpdateGenreDto { Name = "Updated", Description = "Updated Desc" };

            // act
            var result = await _controller.UpdateGenreById(1, updateDto);

            // assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteGenreById_ReturnsNoContent_WhenSuccessful()
        {
            // act
            var result = await _controller.DeleteGenreById(1);

            // assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
