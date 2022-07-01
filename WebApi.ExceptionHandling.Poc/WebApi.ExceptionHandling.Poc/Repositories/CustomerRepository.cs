using WebApi.ExceptionHandling.Poc.Models;

namespace WebApi.ExceptionHandling.Poc.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<Customer> CreateAsync(Customer customer)
        {
            return await Task.FromResult(customer);
        }
    }
}
