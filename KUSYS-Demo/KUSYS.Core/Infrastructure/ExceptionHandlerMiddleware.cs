using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Text.Json;

namespace KUSYS.Core.Infrastructure
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionHandlerMiddleware>();
    }

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var messages = new List<string>();
                SetMessage(ex, messages);
                var response = GetResponse(ex, context);
                await response.WriteAsync(JsonSerializer.Serialize(messages));
            }
        }

        private HttpResponse GetResponse(Exception exception, HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            if (exception.GetType() == typeof(ValidationException) || exception.GetType() == typeof(ApplicationException))
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            else if (exception.GetType() == typeof(UnauthorizedAccessException) || exception.GetType() == typeof(SecurityException))
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return response;
        }

        private void SetMessage(Exception ex, List<string> messages)
        {
            messages.Add(ex.Message);
            if (ex.InnerException != null)
                SetMessage(ex.InnerException, messages);
        }
    }
}
