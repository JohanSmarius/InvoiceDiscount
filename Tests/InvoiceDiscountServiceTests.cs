using InvoiceDiscountService;
using InvoiceDiscountService.Repository;
using InvoiceDiscountService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class InvoiceDiscountServiceTests
    {
        private readonly Mock<ILogger<InvoiceDiscountService.InvoiceDiscountService>> _loggerMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock;
        private readonly Mock<IDiscountCalculator> _discountCalculatorMock;
        private readonly InvoiceDiscountService.InvoiceDiscountService _service;

        public InvoiceDiscountServiceTests()
        {
            _loggerMock = new Mock<ILogger<InvoiceDiscountService.InvoiceDiscountService>>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            _discountCalculatorMock = new Mock<IDiscountCalculator>();
            _service = new InvoiceDiscountService.InvoiceDiscountService(_loggerMock.Object, _customerRepositoryMock.Object, _invoiceRepositoryMock.Object, _discountCalculatorMock.Object);
        }

        
        [Fact]
        public async Task Run_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            _customerRepositoryMock.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(new List<Customer>());

            var httpRequestMock = new Mock<HttpRequest>();

            // Act
            var result = await _service.Run(httpRequestMock.Object, 1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Run_ShouldReturnOk_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "John Doe" };
            var invoice = new Invoice { InvoiceId = Guid.NewGuid(), CustomerId = customer.Id, InvoiceAmount = 500, Period = new DateOnly(2023, 1, 1) };
            var discount = 25.0;

            _customerRepositoryMock.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(new List<Customer> { customer });
            _invoiceRepositoryMock.Setup(repo => repo.GetLastInvoiceByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(invoice);
            _discountCalculatorMock.Setup(calc => calc.CalculateDiscount(It.IsAny<Customer>(), It.IsAny<Invoice>())).ReturnsAsync(discount);

            var httpRequestMock = new Mock<HttpRequest>();

            // Act
            var result = await _service.Run(httpRequestMock.Object, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<double>(okResult.Value);
            Assert.Equal(discount, returnValue);
        }

        [Fact]
        public async Task Run_ShouldReturnCorrectDiscount_WhenInvoiceAmountIsGreaterThan100()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "John Doe" };
            var invoice = new Invoice { InvoiceId = Guid.NewGuid(), CustomerId = customer.Id, InvoiceAmount = 150, Period = new DateOnly(2023, 1, 1) };
            var discount = 7.5;

            _customerRepositoryMock.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(new List<Customer> { customer });
            _invoiceRepositoryMock.Setup(repo => repo.GetLastInvoiceByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(invoice);
            _discountCalculatorMock.Setup(calc => calc.CalculateDiscount(It.IsAny<Customer>(), It.IsAny<Invoice>())).ReturnsAsync(discount);

            var httpRequestMock = new Mock<HttpRequest>();

            // Act
            var result = await _service.Run(httpRequestMock.Object, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<double>(okResult.Value);
            Assert.Equal(discount, returnValue);
        }

        [Fact]
        public async Task Run_ShouldReturnCorrectDiscount_WhenInvoiceAmountIsGreaterThan1000()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "John Doe" };
            var invoice = new Invoice { InvoiceId = Guid.NewGuid(), CustomerId = customer.Id, InvoiceAmount = 1200, Period = new DateOnly(2023, 1, 1) };
            var discount = 120.0;

            _customerRepositoryMock.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(new List<Customer> { customer });
            _invoiceRepositoryMock.Setup(repo => repo.GetLastInvoiceByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(invoice);
            _discountCalculatorMock.Setup(calc => calc.CalculateDiscount(It.IsAny<Customer>(), It.IsAny<Invoice>())).ReturnsAsync(discount);

            var httpRequestMock = new Mock<HttpRequest>();

            // Act
            var result = await _service.Run(httpRequestMock.Object, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<double>(okResult.Value);
            Assert.Equal(discount, returnValue);
        }

    }
}

