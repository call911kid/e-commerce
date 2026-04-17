using Common.Exceptions;
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

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = _env.IsDevelopment() ? exception.Message : "An error occurred while processing your request.",
                ErrorCode = GetErrorCode(exception),
                StackTrace = _env.IsDevelopment() ? exception.StackTrace : null
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private static HttpStatusCode GetStatusCode(Exception exception) => exception switch
        {
            EntityNotFoundException => HttpStatusCode.NotFound,
            ProductNotFoundException => HttpStatusCode.NotFound,
            CustomerNotFoundException => HttpStatusCode.NotFound,
            InvalidCartException => HttpStatusCode.BadRequest,
            InsufficientStockException => HttpStatusCode.Conflict,
            OrderProcessingException => HttpStatusCode.UnprocessableEntity,
            DomainException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        private static string GetErrorCode(Exception exception) => exception switch
        {
            EntityNotFoundException => "ENTITY_NOT_FOUND",
            ProductNotFoundException => "PRODUCT_NOT_FOUND",
            CustomerNotFoundException => "CUSTOMER_NOT_FOUND",
            InvalidCartException => "INVALID_CART",
            InsufficientStockException => "INSUFFICIENT_STOCK",
            OrderProcessingException => "ORDER_PROCESSING_FAILED",
            DomainException => "DOMAIN_ERROR",
            _ => "INTERNAL_SERVER_ERROR"
        };
    }
}
