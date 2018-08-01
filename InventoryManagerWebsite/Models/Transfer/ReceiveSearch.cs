using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagerWebsite.Models.Transfer
{
    public class ReceiveSearch
    {
        public string SearchText { get; set; }

        public List<InvoiceDto> Invoices { get; set; }


    }
}