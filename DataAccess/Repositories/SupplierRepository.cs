using DataAccess.DTO;
using DataAccess.Interface;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        public List<SupplierDto> GetSuppliers(int? supplierTypeId)
        {
            List<Supplier> suppliers;
            using (var ctx = new StoreEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                if (supplierTypeId.HasValue)
                {
                    suppliers = ctx.Suppliers.Where(x => x.SupplierTypeId == supplierTypeId.Value).ToList();
                }
                else
                {
                    suppliers = ctx.Suppliers.ToList();
                }
                
            }

            return AutoMapper.Mapper.Map<List<SupplierDto>>(suppliers);
        }
    }
}
