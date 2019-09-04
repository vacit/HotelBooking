using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.WebUI.Hotel
{
    public class HotelVM : IModel
    {
        public HotelVM()
        {
            this.IsDeleted = false;
            this.IsActive = false;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string HotelUrl { get; set; }
        [Required]
        public int HotelTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
