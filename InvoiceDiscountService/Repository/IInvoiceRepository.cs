namespace InvoiceDiscountService.Repository;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(int customerId);
    Task<Invoice> GetLastInvoiceByCustomerIdAsync(int customerId);
}
