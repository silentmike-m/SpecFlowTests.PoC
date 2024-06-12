namespace SpecFlowTests.PoC.WebApi.Filters;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SpecFlowTests.PoC.WebApi.Exceptions;

internal sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<ApiExceptionFilterAttribute> logger;
    private ProblemDetailsFactory problemDetailsFactory;

    public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger, ProblemDetailsFactory problemDetailsFactory)
    {
        this.logger = logger;
        this.problemDetailsFactory = problemDetailsFactory;
    }

    public override void OnException(ExceptionContext context)
    {
        this.HandleException(context);

        base.OnException(context);
    }

    private ProblemDetails CreateProblemDetails(ExceptionContext context, HttpStatusCode statusCode, string title, string? detail = null)
        => this.problemDetailsFactory.CreateProblemDetails(context.HttpContext, (int)statusCode, title, detail: detail ?? context.Exception.Message);

    private void HandleException(ExceptionContext context)
    {
        this.logger.LogError(context.Exception, "{Message}", context.Exception.Message);

        var problemDetails = context.Exception switch
        {
            EntityNotFoundException => this.CreateProblemDetails(context, HttpStatusCode.BadRequest, "Entity not found"),
            _ => this.CreateProblemDetails(context, HttpStatusCode.InternalServerError, "Unexpected error"),
        };

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
        };

        context.ExceptionHandled = true;
    }
}
