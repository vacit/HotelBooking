using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entity
{
    public class HotelDefinition
    {

        public HotelDefinition()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string HotelUrl { get; set; }
        public int HotelTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
