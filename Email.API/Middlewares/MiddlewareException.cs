using System;
using System.Collections.Generic;
using System.Net;
using Email.Application.Exceptions;
using Email.Application.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Email.API.Middlewares
{
    public static class MiddlewareException
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    var message = error?.Message;

                    var response = context.Response;
                    if (error is ArgumentNullException)
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    else if (error is EmailException)
                    {
                        response.StatusCode = (int)(error as EmailException).StatusCode;
                    }
                    else if (error is BaseApplicationException)
                    {
                        response.StatusCode = (int)(error as BaseApplicationException).StatusCode;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = "Internal Server Error.";
                    }

                    logger.LogError($"Something went wrong: {error}");
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(new Error()
                    {
                        StatusCode = context.Response.StatusCode,
                        Errors = new List<string>() { message }
                    }.ToString());
                });
            });
        }
    }
}
