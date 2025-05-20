using System;
using System.Collections.Generic;

namespace HotelReservationDataLayer.Model
{
    // Nested class representing the structure of each room in the JSON data

    public class RoomReserveDTO
    {
        public int HotelID { get; set; }
        public decimal Price { get; set; }
        public int RoomTypeID { get; set; }
        public string Title { get; set; }
        public int MaxPeople { get; set; }
        public string Description { get; set; }
        public List<RoomNumberDTO> RoomNumbers { get; set; } // This should be a list, not a string.

        // Parameterized constructor with parameters using camelCase
        public RoomReserveDTO(
            int HotelID,
           decimal Price,
           int RoomTypeID,
           string Title,
           int MaxPeople,
           string Description,
           List<RoomNumberDTO> RoomNumbers)
        {
            this.HotelID = HotelID;
            this.Price = Price;
            this.RoomTypeID = RoomTypeID;
            this.Title = Title;
            this.MaxPeople = MaxPeople;
            this.Description = Description;
            this.RoomNumbers = RoomNumbers;
        }


        public class RoomNumberDTO
        {
            public int RoomID { get; set; }
            public string RoomNumber { get; set; }
            public List<UnavailableDateDTO> UnavailableDates { get; set; }
        }

        // Nested class representing the structure of unavailable dates in the JSON data
        public class UnavailableDateDTO
        {
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public string Reason { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
