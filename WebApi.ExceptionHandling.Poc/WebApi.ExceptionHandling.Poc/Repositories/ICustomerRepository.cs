using WebApi.ExceptionHandling.Poc.Models;

namespace WebApi.ExceptionHandling.Poc.Repositories
{
    public interface ICustomerRepository
    {

        Task<Customer> CreateAsync(Customer customer);
    }
}
