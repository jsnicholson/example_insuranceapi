using Api.Exceptions;

namespace Api.Handler {
    public class ExceptionHandlerMiddleware {
        private readonly RequestDelegate _next;
        private readonly GlobalExceptionHandler _exceptionHandler;

        public ExceptionHandlerMiddleware(RequestDelegate next, GlobalExceptionHandler exceptionHandler) {
            _next = next;
            _exceptionHandler = exceptionHandler;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                await _exceptionHandler.TryHandleAsync(context, ex, context.RequestAborted);
            }
        }
    }
}
