
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class HotelsDTO
                {
                    	 public int HotelID  {get; set;}
	 public int HotelTypeID  {get; set;}
	 public string Name  {get; set;}
	 public string City  {get; set;}
	 public string Address  {get; set;}
	 public string Distance  {get; set;}
	 public string Title  {get; set;}
	 public string Description  {get; set;}
	 public decimal Rating  {get; set;}
	 public bool Featured  {get; set;}
	 public DateTime CreatedAt  {get; set;}
	 public DateTime UpdateAt  {get; set;}
            
                    
            public HotelsDTO( int  HotelID,  int  HotelTypeID,  string  Name,  string  City,  string  Address,  string  Distance,  string  Title,  string  Description,  decimal  Rating,  bool  Featured,  DateTime  CreatedAt,  DateTime  UpdateAt){
this.HotelID = HotelID ;
this.HotelTypeID = HotelTypeID ;
this.Name = Name ;
this.City = City ;
this.Address = Address ;
this.Distance = Distance ;
this.Title = Title ;
this.Description = Description ;
this.Rating = Rating ;
this.Featured = Featured ;
this.CreatedAt = CreatedAt ;
this.UpdateAt = UpdateAt ;   
                }
                }
                
                
                 public  class clsHotelsData
                 {
                           
                          public static List<HotelsDTO> GetAllHotels()
{

            List<HotelsDTO> hotelsList = new List<HotelsDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllHotels()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var hotels = new HotelsDTO(
                                						 HotelID:(int)reader ["HotelID"] ,
						 HotelTypeID:(int)reader ["HotelTypeID"] ,
						 Name:(string)reader ["Name"] ,
						 City:(string)reader ["City"] ,
						 Address:(string)reader ["Address"] ,
						 Distance:(string)reader ["Distance"] ,
						 Title:(string)reader ["Title"] ,
						 Description:(string)reader ["Description"] ,
						 Rating:(decimal)reader ["Rating"] ,
						 Featured:(bool)reader ["Featured"] ,
						 CreatedAt:(DateTime)reader ["CreatedAt"] ,
						 UpdateAt:(DateTime)reader ["UpdateAt"] ,

                            );

                            hotelsList.Add(hotels);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return hotelsList;
            }
               
           
}


                          public static Nullable<int> AddNewHotels(HotelsDTO hotels)
{

            Nullable<int> NewHotelsID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewHotels", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@HotelTypeID", hotels.HotelTypeID);
						command.Parameters.AddWithValue("@Name", hotels.Name);
						command.Parameters.AddWithValue("@City", hotels.City);
						command.Parameters.AddWithValue("@Address", hotels.Address);
						command.Parameters.AddWithValue("@Distance", hotels.Distance);
						command.Parameters.AddWithValue("@Title", hotels.Title);
						command.Parameters.AddWithValue("@Description", hotels.Description);
						command.Parameters.AddWithValue("@Rating", hotels.Rating);
						command.Parameters.AddWithValue("@Featured", hotels.Featured);
						command.Parameters.AddWithValue("@CreatedAt", hotels.CreatedAt);
						command.Parameters.AddWithValue("@UpdateAt", hotels.UpdateAt);
;
                        SqlParameter outputIdParam = new SqlParameter("@HotelID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewHotelsID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewHotelsID;
            }
            
        
}
 

                          public static HotelsDTO GetHotelsInfoByID(int HotelID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetHotelsInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelID", HotelID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new HotelsDTO(

                            						 HotelID:(int)reader ["HotelID"],
						 HotelTypeID:(int)reader ["HotelTypeID"],
						 Name:(string)reader ["Name"],
						 City:(string)reader ["City"],
						 Address:(string)reader ["Address"],
						 Distance:(string)reader ["Distance"],
						 Title:(string)reader ["Title"],
						 Description:(string)reader ["Description"],
						 Rating:(decimal)reader ["Rating"],
						 Featured:(bool)reader ["Featured"],
						 CreatedAt:(DateTime)reader ["CreatedAt"],
						 UpdateAt:(DateTime)reader ["UpdateAt"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool UpdateHotels(HotelsDTO hotels)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateHotelsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelID", hotels.HotelID);
						command.Parameters.AddWithValue("@HotelTypeID", hotels.HotelTypeID);
						command.Parameters.AddWithValue("@Name", hotels.Name);
						command.Parameters.AddWithValue("@City", hotels.City);
						command.Parameters.AddWithValue("@Address", hotels.Address);
						command.Parameters.AddWithValue("@Distance", hotels.Distance);
						command.Parameters.AddWithValue("@Title", hotels.Title);
						command.Parameters.AddWithValue("@Description", hotels.Description);
						command.Parameters.AddWithValue("@Rating", hotels.Rating);
						command.Parameters.AddWithValue("@Featured", hotels.Featured);
						command.Parameters.AddWithValue("@CreatedAt", hotels.CreatedAt);
						command.Parameters.AddWithValue("@UpdateAt", hotels.UpdateAt);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeleteHotels(int HotelID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteHotels", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelID", HotelID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             