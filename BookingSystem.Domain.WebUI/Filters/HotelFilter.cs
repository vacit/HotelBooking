using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.WebUI.Filters
{
    public class HotelFilter
    {
        [DisplayName("Hotel Name")]
        public string Name { get; set; }

        [DisplayName("Hotel Type")]
        public int HotelTypeId { get; set; }

    }
}
