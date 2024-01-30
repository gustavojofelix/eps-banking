using cnet_oykryo.domain.Entities;
using cnet_oykryo.domain.Services;
using cnet_oykryo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.application.Services
{
    // CustomerService: Handles operations related to customers
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public void AddCustomer(string name)
        {
            // Business logic for creating a new customer

            // Validate input
            ValidateCustomerName(name);

            // Create a new customer
            var newCustomer = new Customer
            {
                Name = name
                // Additional properties can be set here
            };

            // Optionally, you might want to perform other actions like generating a customer ID,
            // triggering domain events, or persisting changes to the database.

            // Example: Generating a customer ID
            newCustomer.Id = GenerateCustomerId();

            // Example: Triggering a domain event
            // DomainEvents.Raise(new CustomerCreatedEvent(newCustomer));

            // Note: In a real-world application, you would likely handle database persistence in the Infrastructure layer.
            _customerRepository.AddCustomerAsync(newCustomer);
        }

        public Customer GetCustomerByName(string customerName)
        {
            // Validate input
            ValidateCustomerName(customerName);

            // Retrieve the customer from the repository or data store
            Customer customer = _customerRepository.GetCustomerByName(customerName);

            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            // Retrieve the customer from the repository or data store
            Customer customer = await _customerRepository.GetCustomerByIdAsync(customerId); 

            return customer;
        }

        private void ValidateCustomerName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Customer name cannot be null or empty.", nameof(name));
            }
        }

        private int GenerateCustomerId()
        {
            // Placeholder for generating a unique customer ID
            // This could involve some logic to ensure uniqueness

            // For demonstration purposes, a simple random number generator is used here.
            Random random = new Random();
            return random.Next(1000, 9999);
        }
    }

}
