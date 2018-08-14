using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class TransferDto
    {
        public long Id { get; set; }
        public System.DateTimeOffset ActivityTime { get; set; }
        public int ProductId { get; set; }
        public int SourceLocationId { get; set; }
        public int DestinationLocationId { get; set; }
        public int Quantity { get; set; }

        public virtual LocationDto DestinationLocation { get; set; }
        public virtual LocationDto SourceLocation { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
