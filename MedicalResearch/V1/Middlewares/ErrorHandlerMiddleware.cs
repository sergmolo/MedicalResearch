﻿using FluentValidation;
using MedicalResearch.Business.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
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
                        response.StatusCode = e.Errors
                            .Any(f => f.CustomState is NotFound) ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest;

                        responseModel = CommandResult.Failed(
                            e.Errors.Select(
                                e => new CommandError(
                                    e.PropertyName.Replace(".", "") + e.ErrorCode,
                                    e.ErrorMessage)));
                        break;

                    case BusinessLogicException e:
                        response.StatusCode = e.NotFound ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest;
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
