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
        public int LocationId { get; set; }
        public Nullable<short> DirectionId { get; set; }
        public int OriginalQuantity { get; set; }
        public int NewQuantity { get; set; }

        public virtual DirectionDto Direction { get; set; }
        public virtual LocationDto Location { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
