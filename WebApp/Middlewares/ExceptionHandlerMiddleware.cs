using C.Common.Exceptions;
using C.Common.GlobalResponses;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace WebApp.Middlewares
{   

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                switch (error)
                {
                    case NotFoundException:
                        var message = new List<string>() { error.Message };
                        await WriteError(context, HttpStatusCode.NotFound, message);
                        break;

                    case InvalidClientException:
                        message = new List<string>() { error.Message };
                        await WriteError(context, HttpStatusCode.BadRequest, message);
                        break;

                    case BadRequestException:
                        message = new List<string>() { error.Message };
                        await WriteError(context, HttpStatusCode.BadRequest, message); ;
                        break;

                    case PermissionDeniedErrorException:
                        message = new List<string>() { error.Message };
                        await WriteError(context, HttpStatusCode.Forbidden, message);
                        break;


                    case ValidationException ex:
                        await WriteValidationErrors(context, HttpStatusCode.BadRequest, ex);
                        break;

                    default:
                        message = new List<string>() { error.Message };
                        await WriteError(context, HttpStatusCode.InternalServerError, message);
                        break;
                }

                static async Task WriteError(HttpContext context, HttpStatusCode statusCode, List<string> messages)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = (int)statusCode;
                    context.Response.ContentType = "application/json; charset=utf-8";

                    var options = new JsonSerializerOptions() { /*PropertyNamingPolicy = JsonNamingPolicy.CamelCase */};
                    var json = JsonSerializer.Serialize(new ResponseModel(messages), options);
                    await context.Response.WriteAsync(json);
                }

                static async Task WriteValidationErrors(HttpContext context, HttpStatusCode statusCode, ValidationException ex)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = (int)statusCode;
                    context.Response.ContentType = "application/json; charset=utf-8";

                    var validationErrors = ex.Errors.Select(e=>e.ErrorMessage).ToList();
                   // var errorMessages = new List<string>();

                    //foreach (var error in validationErrors)
                    //{
                    //    // Add each error message to the list
                    //    errorMessages.Add(error.message);
                    //}

                    var responseModel = new ResponseModel(validationErrors)
                    {
                        IsSuccess = false  // Indicate failure
                    };

                    var options = new JsonSerializerOptions() { /*PropertyNamingPolicy = JsonNamingPolicy.CamelCase */};
                    // var json = JsonSerializer.Serialize(new { errors = validationErrors });
                    var json = JsonSerializer.Serialize(responseModel, options);

                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
