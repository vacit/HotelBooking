using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class Hotel : EntityBase
    {
        public Hotel()
        {
            this.IsDeleted = false;
            this.IsActive = false;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string HotelUrl { get; set; }
        public int HotelTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}
