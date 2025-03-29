
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class HotelPhotosDTO
                {
                    	 public int HotelPhotoID  {get; set;}
	 public int HotelID  {get; set;}
	 public string PhotoURL  {get; set;}
            
                    
            public HotelPhotosDTO( int  HotelPhotoID,  int  HotelID,  string  PhotoURL){
this.HotelPhotoID = HotelPhotoID ;
this.HotelID = HotelID ;
this.PhotoURL = PhotoURL ;   
                }
                }
                
                
                 public  class clsHotelPhotosData
                 {
                           
                          public static List<HotelPhotosDTO> GetAllHotelPhotos()
{

            List<HotelPhotosDTO> hotelphotosList = new List<HotelPhotosDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllHotelPhotos()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var hotelphotos = new HotelPhotosDTO(
                                						 HotelPhotoID:(int)reader ["HotelPhotoID"] ,
						 HotelID:(int)reader ["HotelID"] ,
						 PhotoURL:(string)reader ["PhotoURL"] ,

                            );

                            hotelphotosList.Add(hotelphotos);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return hotelphotosList;
            }
               
           
}


                          public static Nullable<int> AddNewHotelPhotos(HotelPhotosDTO hotelphotos)
{

            Nullable<int> NewHotelPhotosID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewHotelPhotos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@HotelID", hotelphotos.HotelID);
						command.Parameters.AddWithValue("@PhotoURL", hotelphotos.PhotoURL);
;
                        SqlParameter outputIdParam = new SqlParameter("@HotelPhotoID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewHotelPhotosID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewHotelPhotosID;
            }
            
        
}
 

                          public static HotelPhotosDTO GetHotelPhotosInfoByID(int HotelPhotoID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetHotelPhotosInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelPhotoID", HotelPhotoID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new HotelPhotosDTO(

                            						 HotelPhotoID:(int)reader ["HotelPhotoID"],
						 HotelID:(int)reader ["HotelID"],
						 PhotoURL:(string)reader ["PhotoURL"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool UpdateHotelPhotos(HotelPhotosDTO hotelphotos)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateHotelPhotosByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelPhotoID", hotelphotos.HotelPhotoID);
						command.Parameters.AddWithValue("@HotelID", hotelphotos.HotelID);
						command.Parameters.AddWithValue("@PhotoURL", hotelphotos.PhotoURL);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeleteHotelPhotos(int HotelPhotoID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteHotelPhotos", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelPhotoID", HotelPhotoID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             