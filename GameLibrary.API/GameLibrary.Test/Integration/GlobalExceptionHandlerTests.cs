using GameLibrary.Integration.Handlers;
using GameLibrary.Integration.Exceptions;
using Microsoft.AspNetCore.Http;
using Xunit;
using System.Net.Http;

namespace GameLibrary.Test.Integration
{
    public class GlobalExceptionHandlerTests
    {
        private readonly GlobalExceptionHandler _handler;

        public GlobalExceptionHandlerTests()
        {
            _handler = new GlobalExceptionHandler();
        }

        [Fact]
        public async Task TryHandleAsync_ShouldReturn404_ForEntityNotFoundException()
        {
            // arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new EntityNotFoundException("Test", 1);

            // act
            await _handler.TryHandleAsync(context, exception, default);

            // assert
            Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
        }

        [Fact]
        public async Task TryHandleAsync_ShouldReturn400_ForArgumentException()
        {
            // arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new ArgumentException("Error");

            // act
            await _handler.TryHandleAsync(context, exception, default);

            // assert
            Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
        }

        [Fact]
        public async Task TryHandleAsync_ShouldReturn502_ForHttpRequestException()
        {
            // arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new HttpRequestException("External API Error");

            // act
            await _handler.TryHandleAsync(context, exception, default);

            // assert
            Assert.Equal(StatusCodes.Status502BadGateway, context.Response.StatusCode);
        }

        [Fact]
        public async Task TryHandleAsync_ShouldReturn500_ForGenericException()
        {
            // arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new Exception("Critical Error");

            // act
            await _handler.TryHandleAsync(context, exception, default);

            // assert
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }
    }
}