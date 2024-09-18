using System.Threading.Tasks;

namespace InvoiceDiscountService.Services
{
    public class DiscountCalculator : IDiscountCalculator
    {
        public Task<double> CalculateDiscount(Customer customer, Invoice invoice)
        {
            double discount = 0.0;

            if (invoice.InvoiceAmount > 1000)
            {
                discount = invoice.InvoiceAmount * 0.10;
            }
            else if (invoice.InvoiceAmount > 100)
            {
                discount = invoice.InvoiceAmount * 0.05;
            }

            return Task.FromResult(discount);
        }
    }
}

