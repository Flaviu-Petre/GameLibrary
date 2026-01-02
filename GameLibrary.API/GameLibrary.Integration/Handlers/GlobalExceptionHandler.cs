using GameLibrary.Integration.Exceptions;
using GameLibrary.Integration.Logger;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameLibrary.Integration.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            string layer = "Unknown";
            var stackTrace = new StackTrace(exception);
            var frame = stackTrace.GetFrame(0);
            var method = frame?.GetMethod();
            var declaringType = method?.DeclaringType?.FullName ?? "";

            if (declaringType.Contains("Repository")) layer = "Repository";
            else if (declaringType.Contains("Service")) layer = "Service";
            else if (declaringType.Contains("Domain")) layer = "Domain";
            else if (declaringType.Contains("API")) layer = "API";

            string logMessage = $"[LAYER: {layer}] {exception.Message}";
            LoggerSingleton.GetInstance().Log(logMessage, LogLevel.Error);

            var problemDetails = new ProblemDetails
            {
                Status = exception switch
                {
                    EntityNotFoundException => StatusCodes.Status404NotFound,
                    ArgumentException or ValidationException => StatusCodes.Status400BadRequest,
                    HttpRequestException => StatusCodes.Status502BadGateway,
                    _ => StatusCodes.Status500InternalServerError
                },
                Title = exception switch
                {
                    HttpRequestException => "External API Error",
                    _ => "An error occurred while processing your request"
                },
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}