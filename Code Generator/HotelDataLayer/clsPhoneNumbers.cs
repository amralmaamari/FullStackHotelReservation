
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class PhoneNumbersDTO
                {
                    	 public int PhoneNumbersID  {get; set;}
	 public int UserID  {get; set;}
	 public string PhoneNumber  {get; set;}
	 public bool IsPrimary  {get; set;}
	 public DateTime CreatedAt  {get; set;}
	 public DateTime UpdatedAt  {get; set;}
            
                    
            public PhoneNumbersDTO( int  PhoneNumbersID,  int  UserID,  string  PhoneNumber,  bool  IsPrimary,  DateTime  CreatedAt,  DateTime  UpdatedAt){
this.PhoneNumbersID = PhoneNumbersID ;
this.UserID = UserID ;
this.PhoneNumber = PhoneNumber ;
this.IsPrimary = IsPrimary ;
this.CreatedAt = CreatedAt ;
this.UpdatedAt = UpdatedAt ;   
                }
                }
                
                
                 public  class clsPhoneNumbersData
                 {
                           
                          public static List<PhoneNumbersDTO> GetAllPhoneNumbers()
{

            List<PhoneNumbersDTO> phonenumbersList = new List<PhoneNumbersDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllPhoneNumbers()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var phonenumbers = new PhoneNumbersDTO(
                                						 PhoneNumbersID:(int)reader ["PhoneNumbersID"] ,
						 UserID:(int)reader ["UserID"] ,
						 PhoneNumber:(string)reader ["PhoneNumber"] ,
						 IsPrimary:(bool)reader ["IsPrimary"] ,
						 CreatedAt:(DateTime)reader ["CreatedAt"] ,
						 UpdatedAt:(DateTime)reader ["UpdatedAt"] ,

                            );

                            phonenumbersList.Add(phonenumbers);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return phonenumbersList;
            }
               
           
}


                          public static Nullable<int> AddNewPhoneNumbers(PhoneNumbersDTO phonenumbers)
{

            Nullable<int> NewPhoneNumbersID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewPhoneNumbers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@UserID", phonenumbers.UserID);
						command.Parameters.AddWithValue("@PhoneNumber", phonenumbers.PhoneNumber);
						command.Parameters.AddWithValue("@IsPrimary", phonenumbers.IsPrimary);
						command.Parameters.AddWithValue("@CreatedAt", phonenumbers.CreatedAt);
						command.Parameters.AddWithValue("@UpdatedAt", phonenumbers.UpdatedAt);
;
                        SqlParameter outputIdParam = new SqlParameter("@PhoneNumbersID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewPhoneNumbersID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewPhoneNumbersID;
            }
            
        
}
 

                          public static PhoneNumbersDTO GetPhoneNumbersInfoByID(int PhoneNumbersID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPhoneNumbersInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PhoneNumbersID", PhoneNumbersID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new PhoneNumbersDTO(

                            						 PhoneNumbersID:(int)reader ["PhoneNumbersID"],
						 UserID:(int)reader ["UserID"],
						 PhoneNumber:(string)reader ["PhoneNumber"],
						 IsPrimary:(bool)reader ["IsPrimary"],
						 CreatedAt:(DateTime)reader ["CreatedAt"],
						 UpdatedAt:(DateTime)reader ["UpdatedAt"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool UpdatePhoneNumbers(PhoneNumbersDTO phonenumbers)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePhoneNumbersByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PhoneNumbersID", phonenumbers.PhoneNumbersID);
						command.Parameters.AddWithValue("@UserID", phonenumbers.UserID);
						command.Parameters.AddWithValue("@PhoneNumber", phonenumbers.PhoneNumber);
						command.Parameters.AddWithValue("@IsPrimary", phonenumbers.IsPrimary);
						command.Parameters.AddWithValue("@CreatedAt", phonenumbers.CreatedAt);
						command.Parameters.AddWithValue("@UpdatedAt", phonenumbers.UpdatedAt);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeletePhoneNumbers(int PhoneNumbersID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeletePhoneNumbers", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PhoneNumbersID", PhoneNumbersID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             