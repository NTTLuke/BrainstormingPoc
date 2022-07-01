using FluentValidation;
using WebApi.ExceptionHandling.Poc.Models;

namespace WebApi.ExceptionHandling.Poc.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c=> c.Name).NotEmpty().WithMessage("Please specify a name");
            RuleFor(c=> c.Email).NotEmpty().WithMessage("Please specify an email"); ;
            RuleFor(c=> c.Email).Must(BeValidEmail).WithMessage("Hey Bro Pippo is not a valid email");
            //RuleFor(c=> c.Age).GreaterThan(0).WithMessage("Age must be greater than zero");
        }

        private bool BeValidEmail(string email)
        {
            if (email.Contains("pippo"))
                return false;

            return true;
        }
    }
}
