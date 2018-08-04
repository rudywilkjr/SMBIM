using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerWebsite.Models.Invoice
{
    public class ReceiveInvoiceViewModel
    {
        public InvoiceDto SelectedInvoice { get; set; }

        public List<LocationDto> Locations { get; set; }

        public SelectList LocationsList => new SelectList(Locations, "Id", "Description");

        public int SelectedLocationId { get; set; }

    }

}