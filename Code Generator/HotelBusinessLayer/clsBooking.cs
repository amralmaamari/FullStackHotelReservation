
            using System;
            using System.Data;
            using System.Data.SqlClient;
            
             namespace Hotel_Business
             {
                 public  class clsBooking
                 {
                           
                          
            public enum enMode {AddNew = 0,Update = 1}
            public static enMode Mode = enMode.AddNew;
            
                          public BookingDTO bookingDTO
{
    get
    {
        return new BookingDTO(
	  this.BookingID, 
	  this.UserID, 
	  this.CheckIn, 
	  this.CheckOut, 
	  this.BookingStatusID, 
	  this.CreatedAt, 
	  this.UpdatedAt, 
       );
    }
}

                          	 public int BookingID  {get; set;}
	 public int UserID  {get; set;}
	 public DateTime CheckIn  {get; set;}
	 public DateTime CheckOut  {get; set;}
	 public int BookingStatusID  {get; set;}
	 public DateTime CreatedAt  {get; set;}
	 public DateTime UpdatedAt  {get; set;}

                          
public clsBooking() { 	
 this.BookingID = -1;
	
 this.UserID = -1;
	
 this.CheckIn = DateTime.Now;
	
 this.CheckOut = DateTime.Now;
	
 this.BookingStatusID = -1;
	
 this.CreatedAt = DateTime.Now;
	
 this.UpdatedAt = DateTime.Now;

    Mode = enMode.AddNew;
}
                          
            public clsBooking(BookingDTO booking, enMode mode = enMode.AddNew ){

this.BookingID = booking.BookingID ;

this.UserID = booking.UserID ;

this.CheckIn = booking.CheckIn ;

this.CheckOut = booking.CheckOut ;

this.BookingStatusID = booking.BookingStatusID ;

this.CreatedAt = booking.CreatedAt ;

this.UpdatedAt = booking.UpdatedAt ;

Mode = mode;
}
                          public static List<BookingDTO> GetAllBooking()
{
return clsBookingData.GetAllBooking();
 
}

                          
             public static clsBooking GetBookingInfoByID(int BookingID)
                    {
                   BookingDTO bookingDTO = clsBookingData.GetBookingInfoByID(BookingID);

                    if (bookingDTO != null)
                    {
                        return new clsBooking(bookingDTO, enMode.Update);
                    }
                    else
                    {
                        return null;
                    }
            }
                
                          private  bool _AddNewBooking()
{

this.BookingID = (int)clsBookingData.AddNewBooking(  this.bookingDTO);
            return (this.BookingID != -1);
             
}

                          private  bool _UpdateBooking()
{

                return (clsBookingData.UpdateBooking(this.bookingDTO));
}

                          public  bool Save()
{

if (Mode == enMode.AddNew)
    {
        if (_AddNewBooking())
        {
            Mode = enMode.Update;
            return true;
        }
        else
            return false;
    }
    else
    {
        return _UpdateBooking();
    }

}

                          public static bool DeleteBooking(int BookingID)
{
return clsBookingData.DeleteBooking(BookingID);
 
}


                        
                 }
             } 
                
            
             