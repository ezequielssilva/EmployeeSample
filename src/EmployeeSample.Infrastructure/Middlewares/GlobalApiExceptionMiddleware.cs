using System.Net;
using System.Text.Json;
using EmployeeSample.Domain.Exceptions;
using EmployeeSample.Infrastructure.Api.Response;
using Microsoft.AspNetCore.Http;

namespace EmployeeSample.Infrastructure.Middlewares;

public class GlobalApiExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalApiExceptionMiddleware(RequestDelegate next) => _next = next;

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

            IDictionary<string, IList<string>>? errors = null;

            switch (exception)
            {
                case ApplicationValidationException e:
                    errors = e.Errors;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NotificationException e:
                    errors = e.Errors;
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;

                case EntityNotFoundException e:
                    errors = new Dictionary<string, IList<string>> { { "NotFound", new List<string> { exception.Message } } };
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    errors = new Dictionary<string, IList<string>> { { "Exception", new List<string> { exception.Message } } };
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new ResponseModel(false, errors));
            await response.WriteAsync(result);
        }
    }
}
