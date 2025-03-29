
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class CountriesDTO
                {
                    	 public int CountryID  {get; set;}
	 public string Name  {get; set;}
	 public string Code  {get; set;}
            
                    
            public CountriesDTO( int  CountryID,  string  Name,  string  Code){
this.CountryID = CountryID ;
this.Name = Name ;
this.Code = Code ;   
                }
                }
                
                
                 public  class clsCountriesData
                 {
                           
                          public static List<CountriesDTO> GetAllCountries()
{

            List<CountriesDTO> countriesList = new List<CountriesDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllCountries()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var countries = new CountriesDTO(
                                						 CountryID:(int)reader ["CountryID"] ,
						 Name:(string)reader ["Name"] ,
						 Code:(string)reader ["Code"] ,

                            );

                            countriesList.Add(countries);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return countriesList;
            }
               
           
}


                          public static Nullable<int> AddNewCountries(CountriesDTO countries)
{

            Nullable<int> NewCountriesID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewCountries", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@Name", countries.Name);
						command.Parameters.AddWithValue("@Code", countries.Code);
;
                        SqlParameter outputIdParam = new SqlParameter("@CountryID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewCountriesID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewCountriesID;
            }
            
        
}
 

                          public static CountriesDTO GetCountriesInfoByID(int CountryID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetCountriesInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@CountryID", CountryID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new CountriesDTO(

                            						 CountryID:(int)reader ["CountryID"],
						 Name:(string)reader ["Name"],
						 Code:(string)reader ["Code"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool UpdateCountries(CountriesDTO countries)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateCountriesByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@CountryID", countries.CountryID);
						command.Parameters.AddWithValue("@Name", countries.Name);
						command.Parameters.AddWithValue("@Code", countries.Code);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeleteCountries(int CountryID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteCountries", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@CountryID", CountryID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             