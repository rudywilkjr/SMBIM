using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class LocationsWithProductDto
    {
        public int LocationId { get; set; }

        public string LocationDescription { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int? QuantityOnHand { get; set; }
    }
}

