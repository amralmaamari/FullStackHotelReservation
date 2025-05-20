
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class PaymentStatusDTO
                {
                    	 public int PaymentStatusID  {get; set;}
	 public string StatusName  {get; set;}
            
                    
            public PaymentStatusDTO( int  PaymentStatusID,  string  StatusName){
this.PaymentStatusID = PaymentStatusID ;
this.StatusName = StatusName ;   
                }
                }
                
                
                 public  class clsPaymentStatusData
                 {
                           
                          public static List<PaymentStatusDTO> GetAllPaymentStatus()
{

            List<PaymentStatusDTO> paymentstatusList = new List<PaymentStatusDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllPaymentStatus()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var paymentstatus = new PaymentStatusDTO(
                                						 PaymentStatusID:(int)reader ["PaymentStatusID"] ,
						 StatusName:(string)reader ["StatusName"] ,

                            );

                            paymentstatusList.Add(paymentstatus);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return paymentstatusList;
            }
               
           
}


                          public static Nullable<int> AddNewPaymentStatus(PaymentStatusDTO paymentstatus)
{

            Nullable<int> NewPaymentStatusID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewPaymentStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@StatusName", paymentstatus.StatusName);
;
                        SqlParameter outputIdParam = new SqlParameter("@PaymentStatusID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewPaymentStatusID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewPaymentStatusID;
            }
            
        
}
 

                          public static PaymentStatusDTO GetPaymentStatusInfoByID(int PaymentStatusID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPaymentStatusInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PaymentStatusID", PaymentStatusID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new PaymentStatusDTO(

                            						 PaymentStatusID:(int)reader ["PaymentStatusID"],
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


                          public static bool UpdatePaymentStatus(PaymentStatusDTO paymentstatus)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePaymentStatusByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PaymentStatusID", paymentstatus.PaymentStatusID);
						command.Parameters.AddWithValue("@StatusName", paymentstatus.StatusName);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeletePaymentStatus(int PaymentStatusID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeletePaymentStatus", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PaymentStatusID", PaymentStatusID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             