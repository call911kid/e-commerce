using Common.Exceptions;
using FluentValidation;
using PL.Responses;
using System.Net;

namespace PL.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var message = GetMessage(exception);
            var errors = GetErrors(exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(ApiResponse.Failure(message, errors));
        }

        private static HttpStatusCode GetStatusCode(Exception exception) => exception switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            EntityNotFoundException => HttpStatusCode.NotFound,
            ProductNotFoundException => HttpStatusCode.NotFound,
            CustomerNotFoundException => HttpStatusCode.NotFound,
            InvalidCartException => HttpStatusCode.BadRequest,
            InsufficientStockException => HttpStatusCode.Conflict,
            OrderProcessingException => HttpStatusCode.UnprocessableEntity,
            DomainException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        private string GetMessage(Exception exception) => exception switch
        {
            ValidationException => "Validation failed.",
            _ => _env.IsDevelopment() ? exception.Message : "An error occurred while processing your request."
        };

        private static List<string> GetErrors(Exception exception) => exception switch
        {
            ValidationException validationException => validationException.Errors
                .Select(error => error.ErrorMessage)
                .ToList(),
            _ => new List<string>()
        };
    }
}
