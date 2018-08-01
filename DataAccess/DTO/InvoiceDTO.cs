using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public short SupplierId { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int TotalItems => InvoiceProducts.Sum(x => x.Quantity);

        public virtual SupplierDto Supplier { get; set; }
        public virtual ICollection<InvoiceProductDto> InvoiceProducts { get; set; }
    }
}
