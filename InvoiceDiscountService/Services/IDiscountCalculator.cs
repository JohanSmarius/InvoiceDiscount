using System.Threading.Tasks;

namespace InvoiceDiscountService.Services
{
    public interface IDiscountCalculator
    {
        Task<double> CalculateDiscount(Customer customer, Invoice invoice);
    }
}
