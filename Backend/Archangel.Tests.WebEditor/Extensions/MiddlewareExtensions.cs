using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Common.Services.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Archangel.Tests.WebEditor.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureCustomMiddleware(this IApplicationBuilder app, ILoggerService loggerService)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        loggerService.LogException(error.Error);

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error.",
                            Error = error.Error.Message
                        }.ToString()).ConfigureAwait(false);
                    }
                });
            });
        }

        private class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public string Error { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
