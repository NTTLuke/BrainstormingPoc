using FluentValidation;
using WebApi.ExceptionHandling.Poc.Models;
using WebApi.ExceptionHandling.Poc.Services;
using WebApi.ExceptionHandling.Poc.Validation;

namespace WebApi.ExceptionHandling.Poc.ExternalServices
{
    public class CreateCustomerListener
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<CreateCustomerListener> logger;

        public CreateCustomerListener(ICustomerService customerService, ILogger<CreateCustomerListener> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
        }

        public async Task CustomerReceived(Customer customer)
        {
            var result = await customerService.CreateAsync(customer);
            if (result.IsFaulted)
                logger.LogError("Failed");
            else
                logger.LogDebug("OK");

        }
    }
}
