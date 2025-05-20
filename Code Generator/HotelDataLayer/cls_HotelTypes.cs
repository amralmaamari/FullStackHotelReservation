
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class HotelTypesDTO
                {
                    	 public int HotelTypeID  {get; set;}
	 public string TypeName  {get; set;}
            
                    
            public HotelTypesDTO( int  HotelTypeID,  string  TypeName){
this.HotelTypeID = HotelTypeID ;
this.TypeName = TypeName ;   
                }
                }
                
                
                 public  class clsHotelTypesData
                 {
                           
                          public static List<HotelTypesDTO> GetAllHotelTypes()
{

            List<HotelTypesDTO> hoteltypesList = new List<HotelTypesDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllHotelTypes()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var hoteltypes = new HotelTypesDTO(
                                						 HotelTypeID:(int)reader ["HotelTypeID"] ,
						 TypeName:(string)reader ["TypeName"] ,

                            );

                            hoteltypesList.Add(hoteltypes);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return hoteltypesList;
            }
               
           
}


                          public static Nullable<int> AddNewHotelTypes(HotelTypesDTO hoteltypes)
{

            Nullable<int> NewHotelTypesID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewHotelTypes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@TypeName", hoteltypes.TypeName);
;
                        SqlParameter outputIdParam = new SqlParameter("@HotelTypeID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewHotelTypesID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewHotelTypesID;
            }
            
        
}
 

                          public static HotelTypesDTO GetHotelTypesInfoByID(int HotelTypeID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetHotelTypesInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelTypeID", HotelTypeID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new HotelTypesDTO(

                            						 HotelTypeID:(int)reader ["HotelTypeID"],
						 TypeName:(string)reader ["TypeName"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool UpdateHotelTypes(HotelTypesDTO hoteltypes)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateHotelTypesByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelTypeID", hoteltypes.HotelTypeID);
						command.Parameters.AddWithValue("@TypeName", hoteltypes.TypeName);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeleteHotelTypes(int HotelTypeID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteHotelTypes", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@HotelTypeID", HotelTypeID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             