using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IInvoiceRepository
    {
        InvoiceDto GetInvoice(int invoiceId);
        List<InvoiceDto> GetInvoices(string searchText);
        List<InvoiceDto> GetOpenInvoices();
        InvoiceProductDto GetInvoiceProduct(int invoiceProductId);
        InvoiceDto SaveNewPurchaseOrder(short supplierId);
        void SaveNewPurchaseOrderProduct(int purchaseOrderId, int productId, decimal productCost, short orderedQuantity);

    }
}
