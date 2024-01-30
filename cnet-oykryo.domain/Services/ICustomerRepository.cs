using cnet_oykryo.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.domain.Services
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByName(string customerName);
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<List<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
    }
}
