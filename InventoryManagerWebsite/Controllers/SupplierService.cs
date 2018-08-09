using DataAccess.DTO;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerWebsite.Controllers
{
    public class SupplierService
    {
        private readonly SupplierRepository _supplierRepository = new SupplierRepository();

        public List<SupplierDto> GetSuppliers(int? supplierTypeId)
        {
            return _supplierRepository.GetSuppliers(supplierTypeId);
        }
    }
}