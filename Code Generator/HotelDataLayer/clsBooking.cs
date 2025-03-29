
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class BookingDTO
                {
                    	 public int BookingID  {get; set;}
	 public int UserID  {get; set;}
	 public int RoomInstancesID  {get; set;}
	 public int BookingStatusID  {get; set;}
	 public int ApplicationStatus  {get; set;}
	 public DateTime CreatedAt  {get; set;}
	 public DateTime UpdatedAt  {get; set;}
	 public decimal PaidFees  {get; set;}
	 public int CreatedByUserID  {get; set;}
            
                    
            public BookingDTO( int  BookingID,  int  UserID,  int  RoomInstancesID,  int  BookingStatusID,  int  ApplicationStatus,  DateTime  CreatedAt,  DateTime  UpdatedAt,  decimal  PaidFees,  int  CreatedByUserID){
this.BookingID = BookingID ;
this.UserID = UserID ;
this.RoomInstancesID = RoomInstancesID ;
this.BookingStatusID = BookingStatusID ;
this.ApplicationStatus = ApplicationStatus ;
this.CreatedAt = CreatedAt ;
this.UpdatedAt = UpdatedAt ;
this.PaidFees = PaidFees ;
this.CreatedByUserID = CreatedByUserID ;   
                }
                }
                
                
                 public  class clsBookingData
                 {
                           
                          public static List<BookingDTO> GetAllBooking()
{

            List<BookingDTO> bookingList = new List<BookingDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllBooking()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var booking = new BookingDTO(
                                						 BookingID:(int)reader ["BookingID"] ,
						 UserID:(int)reader ["UserID"] ,
						 RoomInstancesID:(int)reader ["RoomInstancesID"] ,
						 BookingStatusID:(int)reader ["BookingStatusID"] ,
						 ApplicationStatus:(int)reader ["ApplicationStatus"] ,
						 CreatedAt:(DateTime)reader ["CreatedAt"] ,
						 UpdatedAt:(DateTime)reader ["UpdatedAt"] ,
						 PaidFees:(decimal)reader ["PaidFees"] ,
						 CreatedByUserID:(int)reader ["CreatedByUserID"] ,

                            );

                            bookingList.Add(booking);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return bookingList;
            }
               
           
}


                          public static Nullable<int> AddNewBooking(BookingDTO booking)
{

            Nullable<int> NewBookingID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewBooking", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@UserID", booking.UserID);
						command.Parameters.AddWithValue("@RoomInstancesID", booking.RoomInstancesID);
						command.Parameters.AddWithValue("@BookingStatusID", booking.BookingStatusID);
						command.Parameters.AddWithValue("@ApplicationStatus", booking.ApplicationStatus);
						command.Parameters.AddWithValue("@CreatedAt", booking.CreatedAt);
						command.Parameters.AddWithValue("@UpdatedAt", booking.UpdatedAt);
						command.Parameters.AddWithValue("@PaidFees", booking.PaidFees);
						command.Parameters.AddWithValue("@CreatedByUserID", booking.CreatedByUserID);
;
                        SqlParameter outputIdParam = new SqlParameter("@BookingID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewBookingID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewBookingID;
            }
            
        
}
 

                          public static BookingDTO GetBookingInfoByID(int BookingID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBookingInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@BookingID", BookingID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new BookingDTO(

                            						 BookingID:(int)reader ["BookingID"],
						 UserID:(int)reader ["UserID"],
						 RoomInstancesID:(int)reader ["RoomInstancesID"],
						 BookingStatusID:(int)reader ["BookingStatusID"],
						 ApplicationStatus:(int)reader ["ApplicationStatus"],
						 CreatedAt:(DateTime)reader ["CreatedAt"],
						 UpdatedAt:(DateTime)reader ["UpdatedAt"],
						 PaidFees:(decimal)reader ["PaidFees"],
						 CreatedByUserID:(int)reader ["CreatedByUserID"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool UpdateBooking(BookingDTO booking)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateBookingByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@BookingID", booking.BookingID);
						command.Parameters.AddWithValue("@UserID", booking.UserID);
						command.Parameters.AddWithValue("@RoomInstancesID", booking.RoomInstancesID);
						command.Parameters.AddWithValue("@BookingStatusID", booking.BookingStatusID);
						command.Parameters.AddWithValue("@ApplicationStatus", booking.ApplicationStatus);
						command.Parameters.AddWithValue("@CreatedAt", booking.CreatedAt);
						command.Parameters.AddWithValue("@UpdatedAt", booking.UpdatedAt);
						command.Parameters.AddWithValue("@PaidFees", booking.PaidFees);
						command.Parameters.AddWithValue("@CreatedByUserID", booking.CreatedByUserID);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeleteBooking(int BookingID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteBooking", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@BookingID", BookingID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             