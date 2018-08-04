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
        public virtual SupplierDto Supplier { get; set; }
        public virtual ICollection<InvoiceProductDto> InvoiceProducts { get; set; }

        public int? TotalItems => InvoiceProducts?.Sum(x => x.OrderedQuantity);
        public string InvoiceType => Supplier.SupplierType.Description.Equals("Vendor") ? "Purchase Order" : "Customer RMA";
        public string Status => TotalItems == InvoiceProducts.Sum(x => x.ReceivedQuantity) ? "Completed" : InvoiceProducts.Sum(x => x.ReceivedQuantity) > 0 ? "Partial" : "Open";
    }
}
