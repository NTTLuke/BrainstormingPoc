using FluentValidation;
using LanguageExt.Common;

namespace WebApi.ExceptionHandling.Poc.ExternalServices
{
    public static class LogExtensions
    {

        public static void LogResult<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper, ILogger<object> logger)
        {
            result.Match(c =>
            {
                logger.LogInformation($"Success {mapper(c)}");

                return false;

            }, exc =>
            {
                if (exc is ValidationException validationException)
                {
                    logger.LogError(validationException.Message);
                    return false;
                }

                if (exc is Exception exception)
                {
                    logger.LogError(exception.Message);
                    return false;
                }

                return false;
            });
        }
    }
}
