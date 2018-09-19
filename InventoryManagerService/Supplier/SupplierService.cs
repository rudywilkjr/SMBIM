using DataAccess.DTO;
using DataAccess.Interface;
using InventoryManagerService.Interface;
using System.Collections.Generic;

namespace InventoryManagerService.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public List<SupplierDto> GetSuppliers(int? supplierTypeId)
        {
            return supplierRepository.GetSuppliers(supplierTypeId);
        }
    }
}