using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationDataLayer.Model
{
   public class SignUpDTO
    {
        
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int? CountryID { get; set; } = 1;
            public string Image { get; set; }
            public string City { get; set; }


            
        }
    
}
