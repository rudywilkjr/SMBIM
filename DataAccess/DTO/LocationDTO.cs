using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int Type { get; set; }
        public virtual LocationTypeDto LocationType { get; set; }
    }
}
