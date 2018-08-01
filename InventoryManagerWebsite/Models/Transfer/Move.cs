using System.Collections.Generic;
using DataAccess.DTO;

namespace InventoryManagerWebsite.Models.Transfer
{
    public class MoveModel
    {
        public List<LocationsWithProductDto> ItemsAtLocation { get; set; }

        public List<LocationDto> Locations { get; set; }

        public int SelectedDepartureLocationId { get; set; }

        public int SelectedArrivingLocationId { get; set; }

        public int SelectedInventoryId { get; set; }

        public int SelectedQuantity { get; set; }

        public bool? TransferComplete { get; set; }
    }
}