using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path; 
            Console.WriteLine(message);

            await _next(context);
            watch.Stop();

            message = "[Response HTTP] "+context.Request.Method +" - " +context.Request.Path +" responded " +context.Response.StatusCode+ " in "+ watch.ElapsedMilliseconds + "ms";
            Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            var result = JsonConvert.SerializeObject(new {error = ex.Message }, Formatting.None);
            string message;
            if(ex is FluentValidation.ValidationException)
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = "[ERROR]   HTTP " + context.Request.Method+" - "+context.Response.StatusCode + " Error Message: " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                Console.WriteLine(message);
                return context.Response.WriteAsync(result);
            message = "[ERROR]   HTTP " + context.Request.Method+" - "+context.Response.StatusCode + " Error Message: " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";
            Console.WriteLine(message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
        
    }
}