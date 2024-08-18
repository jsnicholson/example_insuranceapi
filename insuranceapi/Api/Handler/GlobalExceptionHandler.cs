using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Api.Exceptions {
    /// <summary>
    /// Any unhandled exceptions occurring in a decoared class will be captured here
    /// This allows consistent formatting back to consumer
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment environment) {
            _logger = logger;
            _environment = environment;
        }

        public virtual async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
            _logger.LogError($"an exception occurred on {httpContext.Request.Path.Value} exception:{exception.Message} trace:{exception.StackTrace}");

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            object responseBody = 
                (_environment.IsDevelopment()) ? 
                    new {
                        message = exception.Message,
                        stackTrace = exception.StackTrace,
                        innerMessage = exception.InnerException?.Message,
                        innerStackTrace = exception.InnerException?.StackTrace
                    }
                : "An unexpected error occurred";

            await httpContext.Response.WriteAsJsonAsync(responseBody, cancellationToken);

            return true;
        }
    }
}