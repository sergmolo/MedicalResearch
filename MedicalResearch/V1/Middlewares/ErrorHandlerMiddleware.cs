using FluentValidation;
using MedicalResearch.Business.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalResearch.V1.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                CommandResult responseModel;
                switch (exception)
                {
                    case ValidationException e:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        responseModel = CommandResult.Failed(e.Errors.Select(e => e.ErrorMessage));
                        break;
                    case ErrorException e:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        responseModel = e.CommandResult;
                        break;
                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        responseModel = CommandResult.Failed(new string[] { exception.Message });
                        break;
                }
                await response.WriteAsJsonAsync(responseModel);
            }
        }
    }
}
