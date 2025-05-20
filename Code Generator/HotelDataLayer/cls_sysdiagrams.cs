
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class sysdiagramsDTO
                {
                    	 public string name  {get; set;}
	 public int principal_id  {get; set;}
	 public int diagram_id  {get; set;}
	 public int version  {get; set;}
	 public byte[] definition  {get; set;}
            
                    
            public sysdiagramsDTO( string  name,  int  principal_id,  int  diagram_id,  int  version,  byte[]  definition){
this.name = name ;
this.principal_id = principal_id ;
this.diagram_id = diagram_id ;
this.version = version ;
this.definition = definition ;   
                }
                }
                
                
                 public  class clssysdiagramsData
                 {
                           
                          public static List<sysdiagramsDTO> GetAllsysdiagrams()
{

            List<sysdiagramsDTO> sysdiagramsList = new List<sysdiagramsDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllsysdiagrams()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var sysdiagrams = new sysdiagramsDTO(
                                						 name:(string)reader ["name"] ,
						 principal_id:(int)reader ["principal_id"] ,
						 diagram_id:(int)reader ["diagram_id"] ,
						 version:(int)reader ["version"] ,
						 definition:(byte[])reader ["definition"] ,

                            );

                            sysdiagramsList.Add(sysdiagrams);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return sysdiagramsList;
            }
               
           
}


                          public static Nullable<int> AddNewsysdiagrams(sysdiagramsDTO sysdiagrams)
{

            Nullable<int> NewsysdiagramsID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewsysdiagrams", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@name", sysdiagrams.name);
						command.Parameters.AddWithValue("@principal_id", sysdiagrams.principal_id);
						command.Parameters.AddWithValue("@version", sysdiagrams.version);
						command.Parameters.AddWithValue("@definition", sysdiagrams.definition);
;
                        SqlParameter outputIdParam = new SqlParameter("@diagram_id", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewsysdiagramsID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewsysdiagramsID;
            }
            
        
}
 

                          public static sysdiagramsDTO GetsysdiagramsInfoByID(int diagram_id)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetsysdiagramsInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@name", name);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new sysdiagramsDTO(

                            						 name:(string)reader ["name"],
						 principal_id:(int)reader ["principal_id"],
						 diagram_id:(int)reader ["diagram_id"],
						 version:(int)reader ["version"],
						 definition:(byte[])reader ["definition"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool Updatesysdiagrams(sysdiagramsDTO sysdiagrams)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatesysdiagramsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@name", sysdiagrams.name);
						command.Parameters.AddWithValue("@principal_id", sysdiagrams.principal_id);
						command.Parameters.AddWithValue("@diagram_id", sysdiagrams.diagram_id);
						command.Parameters.AddWithValue("@version", sysdiagrams.version);
						command.Parameters.AddWithValue("@definition", sysdiagrams.definition);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool Deletesysdiagrams(int diagram_id)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_Deletesysdiagrams", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@diagram_id", diagram_id);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             