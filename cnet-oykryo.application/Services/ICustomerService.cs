using cnet_oykryo.domain.Entities;

namespace cnet_oykryo.application.Services
{
    public interface ICustomerService
    {
        void AddCustomer(string name);
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Customer GetCustomerByName(string customerName);
    }
}