using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerService.Interface
{
    public interface ISupplierService
    {
        List<SupplierDto> GetSuppliers(int? supplierTypeId);
    }
}
