using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationDataLayer.Model
{
    public class HotelDetailsDTO
    {
    
            public int HotelID { get; set; }
            public string TypeName { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public string Distance { get; set; }
            public string Photos { get; set; } // JSON string or you can use List<Photo>
            public string Rooms { get; set; }  // JSON string or use List<Room>
            public string Title { get; set; }
            public string Description { get; set; }
            public decimal? CheapestPrice { get; set; }
            public bool Featured { get; set; }

        public HotelDetailsDTO(
        int HotelID,
        string TypeName,
        string Name,
        string City,
        string Address,
        string Distance,
        string Photos,
        string Rooms,
        string Title,
        string Description,
        decimal? CheapestPrice,
        bool Featured)
    {
        this.HotelID = HotelID;
        this.TypeName = TypeName;
        this.Name = Name;
        this.City = City;
        this.Address = Address;
        this.Distance = Distance;
        this.Photos = Photos;
        this.Rooms = Rooms;
        this.Title = Title;
        this.Description = Description;
        this.CheapestPrice = CheapestPrice;
        this.Featured = Featured;
    }


    }
}
