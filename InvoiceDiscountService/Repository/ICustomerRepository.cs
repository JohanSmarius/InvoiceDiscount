using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceDiscountService.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
