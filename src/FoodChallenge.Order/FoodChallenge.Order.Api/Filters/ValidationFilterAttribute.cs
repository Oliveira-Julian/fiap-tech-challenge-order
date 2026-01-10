using FoodChallenge.Common.Entities;
using FoodChallenge.Common.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace FoodChallenge.Order.Api.Filters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    private readonly ValidationContext _validationContext;

    public ValidationFilterAttribute(ValidationContext notificationContext)
    {
        _validationContext = notificationContext;
    }

    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_validationContext.HasValidations)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            context.HttpContext.Response.ContentType = "application/json";

            var messages = Resposta.ComFalha(_validationContext.ValidationMessages.Distinct());

            var notifications = JsonConvert.SerializeObject(
              messages,
              new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            await context.HttpContext.Response.WriteAsync(notifications).ConfigureAwait(false);

            return;
        }

        if (!context.ModelState.IsValid)
        {
            UnprocessableEntity(context);
        }

        await next().ConfigureAwait(false);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }

    private static void UnprocessableEntity(ResultExecutingContext context)
    {
        if (context.Result is BadRequestObjectResult result)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            context.HttpContext.Response.ContentType = "application/json";

            var details = (ValidationProblemDetails)result.Value;
            var distinctMessages = details.Errors
                .SelectMany(error => error.Value)
                .Distinct();

            context.Result = new JsonResult(Resposta.ComFalha(distinctMessages));
        }
        else if (context.Result is ObjectResult objectResult && objectResult.Value is ProblemDetails details)
        {
            context.HttpContext.Response.ContentType = "application/json";

            context.Result = new JsonResult(Resposta.ComFalha(details.Title));
        }
    }
}
