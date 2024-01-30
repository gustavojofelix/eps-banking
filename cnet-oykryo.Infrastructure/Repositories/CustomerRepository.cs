using cnet_oykryo.domain.Entities;
using cnet_oykryo.domain.Services;
using cnet_oykryo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly EPSDBContext _dbContext;

        public CustomerRepository(EPSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer GetCustomerByName(string customerName)
        {
            return _dbContext.Customers.SingleOrDefault(c => c.Name == customerName);
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _dbContext.Customers.FindAsync(customerId);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
