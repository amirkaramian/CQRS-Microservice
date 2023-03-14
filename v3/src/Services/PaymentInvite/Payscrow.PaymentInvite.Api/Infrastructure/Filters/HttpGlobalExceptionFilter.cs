namespace Payscrow.PaymentInvite.Api.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Payscrow.PaymentInvite.Api.Infrastructure.ActionResults;
    using Payscrow.PaymentInvite.Application.Common.Exceptions;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(ValidationException))
            {
                var e = context.Exception as ValidationException;
                //var problemDetails = new ValidationProblemDetails()
                //{
                //    Instance = context.HttpContext.Request.Path,
                //    Status = StatusCodes.Status400BadRequest,
                //    Detail = "Please refer to the errors property for additional details."
                //};

                //problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

                //context.Result = new BadRequestObjectResult(problemDetails);
                //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = context.HttpContext.Response;
                //if (response.HasStarted)
                //    throw;

                response.Clear();
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.ContentType = "application/json";

                response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    e.Message,
                    Errors = e.Failures.ToDictionary(error => error.Key, error => error.Value)
                }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                Encoding.Default, context.HttpContext.RequestAborted).Wait();
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error occur.Try it again." }
                };

                if (env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                // Result asigned to a result object but in destiny the response is empty. This is a known bug of .net core 1.1
                // It will be fixed in .net core 1.1.2. See https://github.com/aspnet/Mvc/issues/5594 for more information
                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}
