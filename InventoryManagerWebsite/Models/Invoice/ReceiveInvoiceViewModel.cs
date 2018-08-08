using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerWebsite.Models.Invoice
{
    public class ReceiveInvoiceViewModel
    {
        public InvoiceModel SelectedInvoice { get; set; }

        public List<LocationDto> Locations { get; set; }

        public SelectList LocationsList => new SelectList(Locations, "Id", "Description");

        [Required]
        public int SelectedLocationId { get; set; }

    }

    public class InvoiceModel
    {
        public int Id { get; set; }

        public string InvoiceType { get; set; }

        public DateTime CreationDate { get; set; }

        public int TotalItems { get; set; }

        public string Status { get; set; }

        public ICollection<InvoiceProductModel> InvoiceProducts { get; set; }
    }

    public class InvoiceProductModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public short OrderedQuantity { get; set; }

        public short ReceivedQuantity { get; set; }

        public bool? IsSynced { get; set; }

        public string SyncMessage { get; set; }
    }

}