using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Api.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Api.Handler;

namespace Test.Unit {
    public class GlobalExceptionHandler_Unit {
        [Fact]
        public async Task MiddlewareInvoked_TryHandleAsyncCalled() {
            var mockLogger = new Mock<ILogger<GlobalExceptionHandler>>();
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            var mockExceptionHandler = new Mock<GlobalExceptionHandler>(mockLogger.Object, mockEnvironment.Object);

            var mockRequestDelegate = new Mock<RequestDelegate>();
            mockRequestDelegate
                .Setup(m => m(It.IsAny<HttpContext>()))
                .ThrowsAsync(new Exception("Test exception"));

            var middleware = new ExceptionHandlerMiddleware(mockRequestDelegate.Object, mockExceptionHandler.Object);

            var context = new DefaultHttpContext();
            await middleware.InvokeAsync(context);

            mockExceptionHandler.Verify(eh => eh.TryHandleAsync(
                It.IsAny<HttpContext>(),
                It.IsAny<Exception>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}