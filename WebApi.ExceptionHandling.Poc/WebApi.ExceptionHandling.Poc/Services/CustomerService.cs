using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using WebApi.ExceptionHandling.Poc.ExternalServices;
using WebApi.ExceptionHandling.Poc.Models;
using WebApi.ExceptionHandling.Poc.Repositories;

namespace WebApi.ExceptionHandling.Poc.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IValidator<Customer> _validator;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IValidator<Customer> validator,
                               ICustomerRepository customerRepository,
                               ILogger<CustomerService> logger)
        {
            _validator = validator;
            _customerRepository = customerRepository;
            _logger = logger;
        }
        public async Task<Result<Customer>> CreateAsync(Customer customer)
        {
            var validationResults = await _validator.ValidateAsync(customer);
            if (!validationResults.IsValid)
            {
                var validationException = new ValidationException(validationResults.Errors);
                return new Result<Customer>(validationException);
            }
            try
            {
                var myCalculation = await CalculateSomethingUsingBusinessLogicAsync(customer);
                if (myCalculation < 0)
                {
                    var message = "Calculation failed since the value is less then 0";
                    var result = new Result<Customer>(new ArithmeticException(message));
                    result.LogResult(p=> p.Email, _logger);
                    
                    return result;
                }
            }
            catch (Exception ex)
            {
                var d = new Result<Customer>(ex);
                d.LogResult(p => p.Age, _logger);
                return d;

            }


            return await _customerRepository.CreateAsync(customer);
        }

        private async Task<int> CalculateSomethingUsingBusinessLogicAsync(Customer customer)
        {
            Random rnd = new Random();
            var number = rnd.Next(-10, customer.Age);
            var division = 100 / customer.Age;
            return await Task.FromResult(number);
        }
    }
}
