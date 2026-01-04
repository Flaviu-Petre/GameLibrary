using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using GameLibrary.API.Controllers;
using GameLibrary.Service.Services.Interface;
using GameLibrary.Service.Dtos.Game;
using GameLibrary.Integration.Exceptions;

namespace GameLibrary.Test.Api
{
    public class GameControllerTests
    {
        private readonly Mock<IGameService> _mockService;
        private readonly GameController _controller;

        public GameControllerTests()
        {
            _mockService = new Mock<IGameService>();
            _controller = new GameController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllGames_ReturnsOk()
        {
            _mockService.Setup(s => s.GetAllGamesAsync())
                .ReturnsAsync(new List<GameDto>());

            var result = await _controller.GetAllGames();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetGameById_ReturnsNotFound_WhenNull()
        {
            _mockService.Setup(s => s.GetGameByIdAsync(1)).ReturnsAsync((GameDto?)null);

            var result = await _controller.GetGameById(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("not found", notFound.Value?.ToString());
        }

        [Fact]
        public async Task CreateGame_ReturnsOk_OnSuccess()
        {
            var dto = new CreateGameDto { Title = "New" };
            var resultDto = new GameDto { Title = "New" };
            _mockService.Setup(s => s.CreateGameAsync(dto)).ReturnsAsync(resultDto);

            var result = await _controller.CreateGame(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(resultDto, okResult.Value);
        }

        [Fact]
        public async Task CreateGame_ReturnsBadRequest_OnArgumentException()
        {
            var dto = new CreateGameDto();
            _mockService.Setup(s => s.CreateGameAsync(dto))
                .ThrowsAsync(new ArgumentException("Bad data"));

            var result = await _controller.CreateGame(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteGameById_ReturnsNoContent_OnSuccess()
        {
            var result = await _controller.DeleteGameById(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteGameById_ReturnsNotFound_OnEntityNotFound()
        {
            _mockService.Setup(s => s.DeleteGameByIdAsync(1))
                .ThrowsAsync(new EntityNotFoundException("Game", 1));

            var result = await _controller.DeleteGameById(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateGameById_ReturnsNoContent_OnSuccess()
        {
            var result = await _controller.UpdateGameById(1, new UpdateGameDto());
            Assert.IsType<NoContentResult>(result);
        }
    }
}