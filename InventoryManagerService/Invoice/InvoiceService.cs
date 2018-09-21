using DataAccess.DTO;
using DataAccess.Interface;
using DataAccess.Repositories;
using InventoryManagerService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerService.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IProductRepository productRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, IProductRepository productRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.productRepository = productRepository;
        }

        public List<InvoiceDto> GetInvoices(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                return invoiceRepository.GetInvoices(searchText);
            } else
            {
                return invoiceRepository.GetOpenInvoices();
            }
            
        }

        public InvoiceDto GetInvoice(int invoiceId)
        {
            return invoiceRepository.GetInvoice(invoiceId);
        }

        public InvoiceProductDto GetInvoiceProduct(int invoiceProductId)
        {
            return invoiceRepository.GetInvoiceProduct(invoiceProductId);
        }

        public InvoiceDto SaveNewPurchaseOrder(short supplierId)
        {
            return invoiceRepository.SaveNewPurchaseOrder(supplierId);
        }

        public void SaveNewPurchaseOrderProduct(int purchaseOrderId, int productId, decimal productCost, short orderedQuantity)
        {
            try
            {
                if (purchaseOrderId <= 0) throw new ApplicationException("Invalid Purchase Order ID: " + purchaseOrderId);
                if (productRepository.GetProduct(productId) == null) throw new ApplicationException("Invalid Product ID: " + productId);
                if (productCost < (decimal) 0.00) throw new ApplicationException("Invalid Product Cost. Cost must not be less than $0.00");
                if (orderedQuantity <= 0) throw new ApplicationException("Invalid Order Quantity. Quantity must be greater than 0.");
                invoiceRepository.SaveNewPurchaseOrderProduct(purchaseOrderId, productId, productCost, orderedQuantity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
