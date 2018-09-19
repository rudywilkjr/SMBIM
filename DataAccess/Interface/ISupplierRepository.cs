using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface ISupplierRepository
    {
        List<SupplierDto> GetSuppliers(int? supplierTypeId);
    }
}
