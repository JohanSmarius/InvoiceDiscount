namespace InvoiceDiscountService.Repository;

public class FakeInvoiceRepository : IInvoiceRepository
{
    private readonly List<Invoice> _invoices;

    public FakeInvoiceRepository()
    {
        // Initialize with some fake data
        _invoices = new List<Invoice>
        {
            new Invoice
            {
                InvoiceId = Guid.NewGuid(),
                CustomerId = 1,
                InvoiceAmount = 150.0,
                Period = new DateOnly(2023, 1, 1)
            },
            new Invoice
            {
                InvoiceId = Guid.NewGuid(),
                CustomerId = 2,
                InvoiceAmount = 250.0,
                Period = new DateOnly(2023, 2, 1)
            },
            new Invoice
            {
                InvoiceId = Guid.NewGuid(),
                CustomerId = 1,
                InvoiceAmount = 350.0,
                Period = new DateOnly(2023, 3, 1)
            }
        };
    }

    public Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(int customerId)
    {
        var invoices = _invoices.Where(i => i.CustomerId == customerId);
        return Task.FromResult(invoices.AsEnumerable());
    }

    public Task<Invoice> GetLastInvoiceByCustomerIdAsync(int customerId)
    {
        var lastInvoice = _invoices
            .Where(i => i.CustomerId == customerId)
            .OrderByDescending(i => i.Period)
            .FirstOrDefault();
        return Task.FromResult(lastInvoice);
    }
}

