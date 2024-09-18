using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InvoiceDiscountService.Repository
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers;

        public FakeCustomerRepository()
        {
            var random = new Random();

            _customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Address = "123 Main St",
                    PhoneNumber = "555-1234",
                    BirthDate = new DateTime(1990, 1, 1)
                },
                new Customer
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Address = "456 Elm St",
                    PhoneNumber = "555-5678",
                    BirthDate = new DateTime(1995, 5, 5)
                }
            };
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await Task.FromResult(_customers.Find(c => c.Id == customerId));
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await Task.FromResult(_customers);
        }
    }
}
