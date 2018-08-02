using DataAccess.DTO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class InvoiceRepository
    {
        public List<InvoiceDto> GetInvoices(string searchText)
        {
            List<Invoice> invoiceItems;
            using (var ctx = new StoreEntities())
            {
                int.TryParse(searchText, out int searchTextAsInt);
                invoiceItems = ctx.Invoices
                    .Include("InvoiceProducts.Product")
                    .Include("Supplier.SupplierType")
                    .Where(x => x.Id.Equals(searchTextAsInt) || x.InvoiceProducts.Any(product => product.Product.Name.Equals(searchText))).ToList();
            }

            return AutoMapper.Mapper.Map<List<InvoiceDto>>(invoiceItems);
        }

        public List<InvoiceDto> GetOpenInvoices()
        {
            List<Invoice> invoiceItems;
            using (var ctx = new StoreEntities())
            {
                invoiceItems = ctx.Invoices
                    .Include("InvoiceProducts.Product")
                    .Include("Supplier.SupplierType")
                    .Where(x => x.InvoiceProducts.Sum(y => y.ReceivedQuantity) < x.InvoiceProducts.Sum(y => y.OrderedQuantity)).OrderBy(x => x.CreationDate).ToList();
            }

            return AutoMapper.Mapper.Map<List<InvoiceDto>>(invoiceItems);
        }
    }
}
