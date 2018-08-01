using System.Collections.Generic;
using DataAccess.DTO;

namespace InventoryManagerWebsite.Models.Transfer
{
    public class ReceiveModel
    {
        public List<ProductDto> Products { get; set; }

        public List<LocationDto> DestinationLocations { get; set; }

        public List<LocationDto> SourceLocations { get; set; }

        public int SelectedDestinationLocationId { get; set; }

        public int SelectedSourceLocationId { get; set; }

        public int SelectedProductId { get; set; }

        public int SelectedQuantity { get; set; }

        public bool? TransferComplete { get; set; }

        public string SelectedReceivingType { get; set; }

        public List<InvoiceDto> Invoices { get; set; }

        public InvoiceDto SelectedInvoice { get; set; }
    }
}