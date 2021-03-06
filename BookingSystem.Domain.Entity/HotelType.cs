﻿using BookingSystem.Domain.Entity.BaseEntity;

namespace BookingSystem.Domain.Entity
{
    public class HotelType : EntityBase
    {
        public HotelType()
        {
            this.IsDeleted = false;
            this.IsActive = true;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}