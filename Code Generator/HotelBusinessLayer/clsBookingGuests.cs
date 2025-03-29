
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsBookingGuests
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public BookingGuestsDTO bookingguestsDTO
{
    get
    {
        return new BookingGuestsDTO(
	  this.BookingGuestsID, 
	  this.BookingID, 
	  this.GuestID, 
       );
    }
}

                          	 public int BookingGuestsID  {get; set;}
	 public int BookingID  {get; set;}
	 public int GuestID  {get; set;}

                          
public clsBookingGuests() { 	
 this.BookingGuestsID = -1;
	
 this.BookingID = -1;
	
 this.GuestID = -1;

    Mode = enMode.AddNew;
}
                          
            public clsBookingGuests(BookingGuestsDTO bookingguests, enMode mode = enMode.AddNew ){

this.BookingGuestsID = bookingguests.BookingGuestsID ;

this.BookingID = bookingguests.BookingID ;

this.GuestID = bookingguests.GuestID ;

Mode = mode;
}
                          public static List<BookingGuestsDTO> GetAllBookingGuests()
{
return clsBookingGuestsData.GetAllBookingGuests();
 
}

                          
             public static clsBookingGuests GetBookingGuestsInfoByID(int BookingGuestsID)
                    {
                   BookingGuestsDTO bookingguestsDTO = clsBookingGuestsData.GetBookingGuestsInfoByID(BookingGuestsID);

                    if (bookingguestsDTO != null)
                    {
                        return new clsBookingGuests(bookingguestsDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewBookingGuests()
{

this.BookingGuestsID = (int)clsBookingGuestsData.AddNewBookingGuests(  this.bookingguestsDTO);
            return (this.BookingGuestsID != -1);
             
}

                          private  bool _UpdateBookingGuests()
{

                return (clsBookingGuestsData.UpdateBookingGuests(this.bookingguestsDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewBookingGuests())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdateBookingGuests();
    }

}

                          public static bool DeleteBookingGuests(int BookingGuestsID)
{
return clsBookingGuestsData.DeleteBookingGuests(BookingGuestsID);
 
}


                        
                 }
             } 
                
            
             