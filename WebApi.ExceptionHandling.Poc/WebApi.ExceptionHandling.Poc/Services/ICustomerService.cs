using LanguageExt.Common;
using WebApi.ExceptionHandling.Poc.Models;

namespace WebApi.ExceptionHandling.Poc.Services
{
    public interface ICustomerService
    {

        Task<Result<Customer>> CreateAsync(Customer customer);
    }
}
