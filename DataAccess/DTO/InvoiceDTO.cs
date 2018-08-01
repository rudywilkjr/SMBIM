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

        public virtual SupplierDto Supplier { get; set; }
    }
}
