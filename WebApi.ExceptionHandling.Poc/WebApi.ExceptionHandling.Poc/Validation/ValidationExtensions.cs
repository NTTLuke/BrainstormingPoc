using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.ExceptionHandling.Poc.Validation
{
    public static class ValidationExtensions
    {

        public static ValidationProblemDetails ToProblemDetails(this ValidationException validationException)
        {

            var error = new ValidationProblemDetails
            {
                Type = "about:blank",
                Status = 400
            };

            foreach (var validationFailure in validationException.Errors)
            {
                if (error.Errors.ContainsKey(validationFailure.PropertyName))
                {
                    error.Errors[validationFailure.PropertyName] = error.Errors[validationFailure.PropertyName].Concat(new[] { validationFailure.ErrorMessage }).ToArray();
                    continue;
                }

                error.Errors.Add(new KeyValuePair<string, string[]>(validationFailure.PropertyName, new[] { validationFailure.ErrorMessage }));
            }

            return error;
        }
    }
}
