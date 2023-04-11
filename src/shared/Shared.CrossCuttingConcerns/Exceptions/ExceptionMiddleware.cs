﻿using Microsoft.AspNetCore.Http;
using Shared.CrossCuttingConcerns.Exceptions.Handlers;
using Shared.CrossCuttingConcerns.Logging;
using Shared.CrossCuttingConcerns.Logging.Serilog;
using System.Text.Json;

namespace Shared.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly LoggerServiceBase _loggerService;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, LoggerServiceBase loggerService)
    {
        _next = next;
        _contextAccessor = contextAccessor;
        _loggerService = loggerService;
        _httpExceptionHandler = new HttpExceptionHandler();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }

    private Task LogException(HttpContext context, Exception exception)
    {
        List<LogParameter> logParameters =
            new()
            {
                new LogParameter { Type = context.GetType().Name, Value = exception.ToString() }
            };

        LogDetail logDetail =
            new()
            {
                MethodName = _next.Method.Name,
                Parameters = logParameters,
                User = _contextAccessor.HttpContext?.User.Identity?.Name ?? "?"
            };

        _loggerService.Info(JsonSerializer.Serialize(logDetail));
        return Task.CompletedTask;
    }
}
