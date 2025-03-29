
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
	  this.RoomInstancesID, 
	  this.BookingStatusID, 
	  this.ApplicationStatus, 
	  this.CreatedAt, 
	  this.UpdatedAt, 
	  this.PaidFees, 
	  this.CreatedByUserID, 
       );
    }
}

                          	 public int BookingID  {get; set;}
	 public int UserID  {get; set;}
	 public int RoomInstancesID  {get; set;}
	 public int BookingStatusID  {get; set;}
	 public int ApplicationStatus  {get; set;}
	 public DateTime CreatedAt  {get; set;}
	 public DateTime UpdatedAt  {get; set;}
	 public decimal PaidFees  {get; set;}
	 public int CreatedByUserID  {get; set;}

                          
public clsBooking() { 	
 this.BookingID = -1;
	
 this.UserID = -1;
	
 this.RoomInstancesID = -1;
	
 this.BookingStatusID = -1;
	
 this.ApplicationStatus = -1;
	
 this.CreatedAt = DateTime.Now;
	
 this.UpdatedAt = DateTime.Now;
	
 this.PaidFees = 0m;
	
 this.CreatedByUserID = -1;

    Mode = enMode.AddNew;
}
                          
            public clsBooking(BookingDTO booking, enMode mode = enMode.AddNew ){

this.BookingID = booking.BookingID ;

this.UserID = booking.UserID ;

this.RoomInstancesID = booking.RoomInstancesID ;

this.BookingStatusID = booking.BookingStatusID ;

this.ApplicationStatus = booking.ApplicationStatus ;

this.CreatedAt = booking.CreatedAt ;

this.UpdatedAt = booking.UpdatedAt ;

this.PaidFees = booking.PaidFees ;

this.CreatedByUserID = booking.CreatedByUserID ;

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
                
            
             