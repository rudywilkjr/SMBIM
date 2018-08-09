using InventoryManagerWebsite.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerWebsite.Models.PurchaseOrder
{
    public class PurchaseOrderViewModel
    {
        public ICollection<InvoiceModel> PurchaseOrders { get; set; }

    }
}