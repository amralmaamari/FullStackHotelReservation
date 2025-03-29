
            using System;
            using System.Data;
            using Microsoft.Data.SqlClient;

            
             namespace HotelDataAccessLayer
             {
                
                public class PaymentsDTO
                {
                    	 public int PaymentID  {get; set;}
	 public int BookingID  {get; set;}
	 public int PaymentTypeID  {get; set;}
	 public int PaymentStatusID  {get; set;}
	 public decimal Amount  {get; set;}
	 public string CurrencyCode  {get; set;}
	 public DateTime TimeAndDate  {get; set;}
            
                    
            public PaymentsDTO( int  PaymentID,  int  BookingID,  int  PaymentTypeID,  int  PaymentStatusID,  decimal  Amount,  string  CurrencyCode,  DateTime  TimeAndDate){
this.PaymentID = PaymentID ;
this.BookingID = BookingID ;
this.PaymentTypeID = PaymentTypeID ;
this.PaymentStatusID = PaymentStatusID ;
this.Amount = Amount ;
this.CurrencyCode = CurrencyCode ;
this.TimeAndDate = TimeAndDate ;   
                }
                }
                
                
                 public  class clsPaymentsData
                 {
                           
                          public static List<PaymentsDTO> GetAllPayments()
{

            List<PaymentsDTO> paymentsList = new List<PaymentsDTO>();
              using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) {
               connection.Open();

            string Query = "select * From FN_GetAllPayments()";
            try
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var payments = new PaymentsDTO(
                                						 PaymentID:(int)reader ["PaymentID"] ,
						 BookingID:(int)reader ["BookingID"] ,
						 PaymentTypeID:(int)reader ["PaymentTypeID"] ,
						 PaymentStatusID:(int)reader ["PaymentStatusID"] ,
						 Amount:(decimal)reader ["Amount"] ,
						 CurrencyCode:(string)reader ["CurrencyCode"] ,
						 TimeAndDate:(DateTime)reader ["TimeAndDate"] ,

                            );

                            paymentsList.Add(payments);
                        }
                    }

                }
            }
            catch (Exception ex) { }

            return paymentsList;
            }
               
           
}


                          public static Nullable<int> AddNewPayments(PaymentsDTO payments)
{

            Nullable<int> NewPaymentsID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewPayments", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       						command.Parameters.AddWithValue("@BookingID", payments.BookingID);
						command.Parameters.AddWithValue("@PaymentTypeID", payments.PaymentTypeID);
						command.Parameters.AddWithValue("@PaymentStatusID", payments.PaymentStatusID);
						command.Parameters.AddWithValue("@Amount", payments.Amount);
						command.Parameters.AddWithValue("@CurrencyCode", payments.CurrencyCode);
						command.Parameters.AddWithValue("@TimeAndDate", payments.TimeAndDate);
;
                        SqlParameter outputIdParam = new SqlParameter("@PaymentID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewPaymentsID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewPaymentsID;
            }
            
        
}
 

                          public static PaymentsDTO GetPaymentsInfoByID(int PaymentID)
{

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPaymentsInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PaymentID", PaymentID);


                using (SqlDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                            return  new PaymentsDTO(

                            						 PaymentID:(int)reader ["PaymentID"],
						 BookingID:(int)reader ["BookingID"],
						 PaymentTypeID:(int)reader ["PaymentTypeID"],
						 PaymentStatusID:(int)reader ["PaymentStatusID"],
						 Amount:(decimal)reader ["Amount"],
						 CurrencyCode:(string)reader ["CurrencyCode"],
						 TimeAndDate:(DateTime)reader ["TimeAndDate"],

                            );
                        
                     }
                 }

                    }
                }
                catch (Exception ex) { }

                return null;
            }
            
        
}


                          public static bool UpdatePayments(PaymentsDTO payments)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

               
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePaymentsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PaymentID", payments.PaymentID);
						command.Parameters.AddWithValue("@BookingID", payments.BookingID);
						command.Parameters.AddWithValue("@PaymentTypeID", payments.PaymentTypeID);
						command.Parameters.AddWithValue("@PaymentStatusID", payments.PaymentStatusID);
						command.Parameters.AddWithValue("@Amount", payments.Amount);
						command.Parameters.AddWithValue("@CurrencyCode", payments.CurrencyCode);
						command.Parameters.AddWithValue("@TimeAndDate", payments.TimeAndDate);
;
                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}


                          public static bool DeletePayments(int PaymentID)
{

            Nullable<int> rowAffected  = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeletePayments", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                       						command.Parameters.AddWithValue("@PaymentID", PaymentID);

                        rowAffected = command.ExecuteNonQuery();

                        

                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }
            
        
}

                        
                 }
             } 
                
            
             