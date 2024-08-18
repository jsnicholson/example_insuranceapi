using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Handler {
    public class RawRequestHandler : ActionFilterAttribute {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<RawRequestHandler>>();
            context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            var requestBody = await context.HttpContext.Request.Body.ReadAsStringAsync();
            logger.LogInformation($"{context.HttpContext.Request.Path.Value} received body:{requestBody}");
            Console.WriteLine(context.HttpContext.Request);
            await base.OnActionExecutionAsync(context, next);
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next) {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<RawRequestHandler>>();

            await base.OnResultExecutionAsync(context, next);
        }
    }

    public static class RequestExtensions {
        public static async Task<string> ReadAsStringAsync(this Stream requestBody, bool leaveOpen = false) {
            using StreamReader reader = new(requestBody, leaveOpen: leaveOpen);
            var bodyAsString = await reader.ReadToEndAsync();
            return bodyAsString;
        }
    }
}