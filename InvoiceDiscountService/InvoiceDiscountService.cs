using InvoiceDiscountService.Repository;
using InvoiceDiscountService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;

namespace InvoiceDiscountService
{
    public class InvoiceDiscountService
    {
        private readonly ILogger<InvoiceDiscountService> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IDiscountCalculator _discountCalculator; // Injecting IDiscountCalculator

        public InvoiceDiscountService(ILogger<InvoiceDiscountService> logger, ICustomerRepository customerRepository, IInvoiceRepository invoiceRepository, IDiscountCalculator discountCalculator) // Adding IDiscountCalculator parameter
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _invoiceRepository = invoiceRepository;
            _discountCalculator = discountCalculator; // Assigning the injected IDiscountCalculator instance
        }

        [Function("InvoiceDiscountService")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "InvoiceDiscountService/{customerId:int}")] HttpRequest req, int customerId)
        {
            _logger.LogInformation($"Processing request for customerId: {customerId}");

            // Fetch customer details
            var customers = await _customerRepository.GetAllCustomersAsync();
            var customer = customers.FirstOrDefault();

            if (customer == null)
            {
                return new NotFoundResult();
            }

            // Fetch last invoice by customer ID
            var lastInvoice = await _invoiceRepository.GetLastInvoiceByCustomerIdAsync(customerId);

            // Calculate discount using the injected discount calculator
            var discount = await _discountCalculator.CalculateDiscount(customer, lastInvoice);

            // Return the discount value, customer details, invoices, and last invoice
            return new OkObjectResult(discount);

        }
    }
}
