using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class InvoiceProductDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal Cost { get; set; }
        public short Quantity { get; set; }

        public virtual InvoiceDto Invoice { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
