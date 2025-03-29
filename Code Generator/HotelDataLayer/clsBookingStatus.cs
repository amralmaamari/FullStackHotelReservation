
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class BookingStatusDTO
                {
                    	 public int BookingStatusID  {get; set;}
	 public string StatusName  {get; set;}
            
                    
            public BookingStatusDTO( int  BookingStatusID,  string  StatusName){
this.BookingStatusID = BookingStatusID ;
this.StatusName = StatusName ;   
                }
                }
                
                
                 public  class clsBookingStatusData
                 {
                           
                          public static List<BookingStatusDTO> GetAllBookingStatus()
{

            List<BookingStatusDTO> bookingstatusList = new List<BookingStatusDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllBookingStatus()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var bookingstatus = new BookingStatusDTO(
                                						 BookingStatusID:(int)reader ["BookingStatusID"] ,
						 StatusName:(string)reader ["StatusName"] ,

                            );

                            bookingstatusList.Add(bookingstatus);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return bookingstatusList;
            }
               
           
}


                          public static Nullable<int> AddNewBookingStatus(BookingStatusDTO bookingstatus)
{

            Nullable<int> NewBookingStatusID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewBookingStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@StatusName", bookingstatus.StatusName);
;
                        SqlParameter outputIdParam = new SqlParameter("@BookingStatusID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewBookingStatusID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewBookingStatusID;
            }
            
        
}
 

                          public static BookingStatusDTO GetBookingStatusInfoByID(int BookingStatusID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBookingStatusInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@BookingStatusID", BookingStatusID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new BookingStatusDTO(

                            						 BookingStatusID:(int)reader ["BookingStatusID"],
						 StatusName:(string)reader ["StatusName"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool UpdateBookingStatus(BookingStatusDTO bookingstatus)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateBookingStatusByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@BookingStatusID", bookingstatus.BookingStatusID);
						command.Parameters.AddWithValue("@StatusName", bookingstatus.StatusName);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeleteBookingStatus(int BookingStatusID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteBookingStatus", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@BookingStatusID", BookingStatusID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             