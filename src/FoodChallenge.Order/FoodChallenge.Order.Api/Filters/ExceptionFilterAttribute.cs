using FoodChallenge.Common.Entities;
using FoodChallenge.Order.Domain.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FoodChallenge.Order.Api.Filters;

public class ExceptionFilterAttribute : IExceptionFilter
{
    public ExceptionFilterAttribute()
    {
    }

    public void OnException(ExceptionContext context)
    {
        var errorMessage = Textos.ErroInesperado;

#if DEBUG
        errorMessage = context.Exception.Message;
#endif

        var response = Resposta.ComFalha(errorMessage);

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.HttpContext.Response.ContentType = "application/json";
        context.Result = new JsonResult(response);
    }
}
