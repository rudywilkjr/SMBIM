using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class DirectionTypeDto
    {
        public int Id { get; set; }
        public short DirectionId { get; set; }
        public string Description { get; set; }

        public virtual DirectionDto Direction { get; set; }
    }
}
