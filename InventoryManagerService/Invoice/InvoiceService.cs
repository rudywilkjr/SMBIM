using DataAccess.DTO;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerService.Invoice
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepository = new InvoiceRepository();

        public List<InvoiceDto> GetInvoices(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                return _invoiceRepository.GetInvoices(searchText);
            } else
            {
                return _invoiceRepository.GetOpenInvoices();
            }
            
        }
    }
}
