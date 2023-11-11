﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BlackHole._360.Api.Filters;

internal class HttpExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly ILogger _logger;
    private readonly IDictionary<Type, Func<Exception, ObjectResult>> _exceptionBehaviours;

    public int Order => int.MaxValue;

    public HttpExceptionFilter(ILogger<HttpExceptionFilter> logger)
    {
        _logger = logger;
        _exceptionBehaviours = new Dictionary<Type, Func<Exception, ObjectResult>>
        {
            { typeof(SqlException), GetSqlExceptionResult },
            { typeof(NullReferenceException), GetNotFoundExceptionResult },
            { typeof(ArgumentException), GetNotFoundExceptionResult },
        };
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            ObjectResult result;

            _logger.LogError(context.Exception, "An error occured during HTTP request");

            if (_exceptionBehaviours.ContainsKey(context.Exception.GetType()))
            {
                var behaviour = _exceptionBehaviours[context.Exception.GetType()];

                result = behaviour(context.Exception);
            }
            else
            {
                result = new("An unhandled exception occured")
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            context.ExceptionHandled = true;
            context.Result = result;
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // do nothing
    }

    private ObjectResult GetSqlExceptionResult(Exception exception)
        => new("An SQL exception occured")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };

    private ObjectResult GetNotFoundExceptionResult(Exception exception)
        => new(exception.Message)
        {
            StatusCode = StatusCodes.Status404NotFound,
        };
}

