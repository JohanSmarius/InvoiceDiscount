using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDiscountService
{
    public record Customer
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Address { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime BirthDate { get; init; }
    }
}
