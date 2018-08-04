using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerWebsite.Models.Invoice
{
    public class InvoiceSearchViewModel
    {
        public string InvoiceSearchText { get; set; }

        public List<InvoiceDto> Invoices { get; set; }

        public string SelectedInvoiceId { get; set; }
    }
}