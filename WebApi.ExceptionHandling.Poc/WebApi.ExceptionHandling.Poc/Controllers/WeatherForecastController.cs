using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using WebApi.ExceptionHandling.Poc.ExternalServices;
using WebApi.ExceptionHandling.Poc.Models;
using WebApi.ExceptionHandling.Poc.Services;
using WebApi.ExceptionHandling.Poc.Validation;

namespace WebApi.ExceptionHandling.Poc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ICustomerService customerService;
        private readonly CreateCustomerListener createCustomerListener;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ICustomerService customerService,
                                         CreateCustomerListener createCustomerListener,
                                         ILogger<WeatherForecastController> logger)
        {
            this.customerService = customerService;
            this.createCustomerListener = createCustomerListener;
            _logger = logger;
        }

        [HttpPost(Name = "customers")]
        public async Task<IActionResult> Create([FromBody] Customer request, [FromQuery] bool fromExternal)
        {
            if (!fromExternal)
            {
                var result = await customerService.CreateAsync(request);
                result.LogResult(p => p.WelcomeMessage, _logger);
                return result.ToOk(c => c.WelcomeMessage);
            }

            
            await createCustomerListener.CustomerReceived(request);
            return Ok();
        }



    }
}