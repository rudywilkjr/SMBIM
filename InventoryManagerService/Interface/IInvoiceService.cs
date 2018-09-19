using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerService.Interface
{
    public interface IInvoiceService
    {
        List<InvoiceDto> GetInvoices(string searchText);
        InvoiceDto GetInvoice(int invoiceId);
        InvoiceProductDto GetInvoiceProduct(int invoiceProductId);
        InvoiceDto SaveNewPurchaseOrder(short supplierId);
        void SaveNewPurchaseOrderProduct(int purchaseOrderId, int productId, decimal productCost, short orderedQuantity);

    }
}
