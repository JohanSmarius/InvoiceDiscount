using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDiscountService
{
    public class Invoice
    {
        private double _invoiceAmount;

        public Guid InvoiceId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public double InvoiceAmount
        {
            get => _invoiceAmount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(InvoiceAmount), "Invoice amount cannot be negative.");
                }
                _invoiceAmount = value;
            }
        }

        // Always the first of the month
        public DateOnly Period { get; set; }

    }
}
