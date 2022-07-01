

using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using WebApi.ExceptionHandling.Poc.Validation;

namespace WebApi.ExceptionHandling.Poc.Controllers
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="result"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static IActionResult ToOk<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper)
        {
            return result.Match<IActionResult>(c =>
            {
                var response = mapper(c);
                return new OkObjectResult(response);
            }, exc =>
            {
                if (exc is ValidationException validationException)
                {
                    return new BadRequestObjectResult(validationException.ToProblemDetails());
                }

                if (exc is Exception exception)
                {
                    return new BadRequestObjectResult(exception.Message);
                }

                return new StatusCodeResult(500);
            });
        }
    }
}
