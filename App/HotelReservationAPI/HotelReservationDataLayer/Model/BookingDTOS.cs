using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationDataLayer.Model
{
    public class BookingDTOS
    {
        public class RoomDTO
        {
            public int BookingRoomID { get; set; }
            public int RoomID { get; set; }
            public decimal PaidFees { get; set; }
            public string RoomType { get; set; }
            public int HotelID { get; set; }
            public string RoomNumber { get; set; } // كانت int، الآن string حسب البيانات
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public string Reason { get; set; }
            public bool IsActive { get; set; }
        }
        public class BookingDetailsDTO
        {
            public int BookingID { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public int BookingStatusID { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

            public List<RoomDTO> Rooms { get; set; } // هذي أهم نقطة
        }
    }
}
