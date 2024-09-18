using InvoiceDiscountService;
using InvoiceDiscountService.Services;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class DiscountCalculatorTests
    {
        [Fact]
        public async Task CalculateDiscount_ShouldReturnZero_WhenInvoiceAmountIsZero()
        {
            // Arrange
            var customer = new Customer();
            var invoice = new Invoice { InvoiceAmount = 0 };
            var calculator = new DiscountCalculator();

            // Act
            var discount = await calculator.CalculateDiscount(customer, invoice);

            // Assert
            Assert.Equal(0, discount);
        }

        [Fact]
        public async Task CalculateDiscount_ShouldReturnZero_WhenInvoiceAmountIsLessThan100()
        {
            // Arrange
            var customer = new Customer();
            var invoice = new Invoice { InvoiceAmount = 99 };
            var calculator = new DiscountCalculator();

            // Act
            var discount = await calculator.CalculateDiscount(customer, invoice);

            // Assert
            Assert.Equal(0, discount);
        }

        [Fact]
        public async Task CalculateDiscount_ShouldReturnFivePercentDiscount_WhenInvoiceAmountIsGreaterThan100AndLessThan1000()
        {
            // Arrange
            var customer = new Customer();
            var invoice = new Invoice { InvoiceAmount = 500 };
            var calculator = new DiscountCalculator();

            // Act
            var discount = await calculator.CalculateDiscount(customer, invoice);

            // Assert
            Assert.Equal(25, discount);
        }

        [Fact]
        public async Task CalculateDiscount_ShouldReturnTenPercentDiscount_WhenInvoiceAmountIsGreaterThan1000()
        {
            // Arrange
            var customer = new Customer();
            var invoice = new Invoice { InvoiceAmount = 1500 };
            var calculator = new DiscountCalculator();

            // Act
            var discount = await calculator.CalculateDiscount(customer, invoice);

            // Assert
            Assert.Equal(150, discount);
        }
    }
}
