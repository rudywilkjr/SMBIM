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
        private readonly ProductRepository _productRepository = new ProductRepository();

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

        public InvoiceDto GetInvoice(int invoiceId)
        {
            return _invoiceRepository.GetInvoice(invoiceId);
        }

        public InvoiceProductDto GetInvoiceProduct(int invoiceProductId)
        {
            return _invoiceRepository.GetInvoiceProduct(invoiceProductId);
        }

        public InvoiceDto SaveNewPurchaseOrder(short supplierId)
        {
            return _invoiceRepository.SaveNewPurchaseOrder(supplierId);
        }

        public void SaveNewPurchaseOrderProduct(int purchaseOrderId, int productId, decimal productCost, short orderedQuantity)
        {
            try
            {
                if (purchaseOrderId <= 0) throw new ApplicationException("Invalid Purchase Order ID: " + purchaseOrderId);
                if (_productRepository.GetProduct(productId) == null) throw new ApplicationException("Invalid Product ID: " + productId);
                if (productCost < 0) throw new ApplicationException("Invalid Product Cost. Cost must not be less than $0.00");
                if (orderedQuantity <= 0) throw new ApplicationException("Invalid Order Quantity. Quantity must be greater than 0.");
                if (orderedQuantity == 2) throw new ApplicationException("Test Message for 2 Qty.");
                _invoiceRepository.SaveNewPurchaseOrderProduct(purchaseOrderId, productId, productCost, orderedQuantity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
