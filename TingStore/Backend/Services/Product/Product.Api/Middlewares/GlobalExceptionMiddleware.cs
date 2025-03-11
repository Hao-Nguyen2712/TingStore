using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace User.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception){
            _logger.LogError(exception, "An error occurred while processing the request");

            var response = context.Response;
            response.ContentType = "application/problem+json";

            var (statusCode, problemDetails) = CreateProblemDetails(context, exception);

            response.StatusCode = statusCode;
            var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await response.WriteAsync(json);
        }

        private (int statusCode, ProblemDetails problemDetails) CreateProblemDetails(HttpContext context, Exception exception){
            var problemDetails = new ProblemDetails{
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "An error occurred while processing your request",
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path,
                Detail = _env.IsDevelopment() ? exception.Message : "An unexpected error occurred. Please try again later."
            };

            int statusCode = (int)HttpStatusCode.InternalServerError;

            switch(exception){
                case ArgumentNullException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    problemDetails.Status = statusCode;
                    problemDetails.Title = "Invalid Argument";
                    problemDetails.Detail = _env.IsDevelopment() ? exception.Message : "One or more argument are null";
                    break;

                case InvalidOperationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    problemDetails.Status = statusCode;
                    problemDetails.Title = "Invalid Operation";
                    problemDetails.Detail = _env.IsDevelopment() ? exception.Message : "An invalid operation was attempted.";
                    break;

                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Status = statusCode;
                    problemDetails.Title = "Unauthorized Access";
                    problemDetails.Detail = "You are not authorized to perform this action.";
                    break;

                case KeyNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    problemDetails.Status = statusCode;
                    problemDetails.Title = "Resource Not Found";
                    problemDetails.Detail = _env.IsDevelopment() ? exception.Message : "The requested resource was not found.";
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Status = statusCode;
                    break;
            }

            if(_env.IsDevelopment()){
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
            }
            return (statusCode, problemDetails);
        }
    }
    // Extension method để dễ dàng thêm middleware vào pipeline
    public static class GlobalExceptionMiddlewareExtensions{
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
