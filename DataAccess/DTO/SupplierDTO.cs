using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class SupplierDto
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public short SupplierTypeId { get; set; }

        public virtual SupplierTypeDto SupplierType { get; set; }
    }
}
