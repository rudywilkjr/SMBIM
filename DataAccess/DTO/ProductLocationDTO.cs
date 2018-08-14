using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ProductLocationDto
    {
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public Nullable<int> OnHand { get; set; }

        public virtual LocationDto Location { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
