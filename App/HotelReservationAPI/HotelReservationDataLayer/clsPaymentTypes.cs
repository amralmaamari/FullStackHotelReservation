
using System;
using System.Data;
using HotelReservationDataLayer;
using Microsoft.Data.SqlClient;


namespace HotelDataAccessLayer
{

    public class PaymentTypesDTO
    {
        public int PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }


        public PaymentTypesDTO(int PaymentTypeID, string PaymentTypeName)
        {
            this.PaymentTypeID = PaymentTypeID;
            this.PaymentTypeName = PaymentTypeName;
        }
    }


    public class clsPaymentTypesData
    {

        public static List<PaymentTypesDTO> GetAllPaymentTypes()
        {

            List<PaymentTypesDTO> paymenttypesList = new List<PaymentTypesDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllPaymentTypes()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var paymenttypes = new PaymentTypesDTO(
                                                             PaymentTypeID: (int)reader["PaymentTypeID"],
                             PaymentTypeName: (string)reader["PaymentTypeName"]
    

                                );

                                paymenttypesList.Add(paymenttypes);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return paymenttypesList;
            }


        }


        public static Nullable<int> AddNewPaymentTypes(PaymentTypesDTO paymenttypes)
        {

            Nullable<int> NewPaymentTypesID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewPaymentTypes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PaymentTypeName", paymenttypes.PaymentTypeName);
                        ;
                        SqlParameter outputIdParam = new SqlParameter("@PaymentTypeID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewPaymentTypesID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewPaymentTypesID;
            }


        }


        public static PaymentTypesDTO GetPaymentTypesInfoByID(int PaymentTypeID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPaymentTypesInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentTypeID", PaymentTypeID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PaymentTypesDTO(

                                                         PaymentTypeID: (int)reader["PaymentTypeID"],
                             PaymentTypeName: (string)reader["PaymentTypeName"]
    

                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }


        public static bool UpdatePaymentTypes(PaymentTypesDTO paymenttypes)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePaymentTypesByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentTypeID", paymenttypes.PaymentTypeID);
                        command.Parameters.AddWithValue("@PaymentTypeName", paymenttypes.PaymentTypeName);
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static bool DeletePaymentTypes(int PaymentTypeID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeletePaymentTypes", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentTypeID", PaymentTypeID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


    }
}


