
using System;
using System.Data;
using HotelReservationDataLayer;
using Microsoft.Data.SqlClient;


namespace HotelDataAccessLayer
{

    public class RoomUnavailableDatesDTO
    {
        public int RoomUnavailableDateID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }


        public RoomUnavailableDatesDTO(int RoomUnavailableDateID, int RoomID, DateTime CheckIn, DateTime CheckOut, string Reason, bool IsActive)
        {
            this.RoomUnavailableDateID = RoomUnavailableDateID;
            this.RoomID = RoomID;
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.Reason = Reason;
            this.IsActive = IsActive;
        }
    }


    public class clsRoomUnavailableDatesData
    {

        public static List<RoomUnavailableDatesDTO> GetAllRoomUnavailableDates()
        {

            List<RoomUnavailableDatesDTO> roomunavailabledatesList = new List<RoomUnavailableDatesDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllRoomUnavailableDates()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var roomunavailabledates = new RoomUnavailableDatesDTO(
                                                             RoomUnavailableDateID: (int)reader["RoomUnavailableDateID"],
                             RoomID: (int)reader["RoomID"],
                             CheckIn: (DateTime)reader["CheckIn"],
                             CheckOut: (DateTime)reader["CheckOut"],
                             Reason: (string)reader["Reason"],
                             IsActive: (bool)reader["IsActive"]
    

                                );

                                roomunavailabledatesList.Add(roomunavailabledates);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return roomunavailabledatesList;
            }


        }


        public static Nullable<int> AddNewRoomUnavailableDates(RoomUnavailableDatesDTO roomunavailabledates)
        {

            Nullable<int> NewRoomUnavailableDatesID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewRoomUnavailableDates", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomID", roomunavailabledates.RoomID);
                        command.Parameters.AddWithValue("@CheckIn", roomunavailabledates.CheckIn);
                        command.Parameters.AddWithValue("@CheckOut", roomunavailabledates.CheckOut);
                        command.Parameters.AddWithValue("@Reason", roomunavailabledates.Reason);
                        command.Parameters.AddWithValue("@IsActive", roomunavailabledates.IsActive);
                        ;
                        SqlParameter outputIdParam = new SqlParameter("@RoomUnavailableDateID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewRoomUnavailableDatesID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewRoomUnavailableDatesID;
            }


        }


        public static RoomUnavailableDatesDTO GetRoomUnavailableDatesInfoByID(int RoomUnavailableDateID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetRoomUnavailableDatesInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomUnavailableDateID", RoomUnavailableDateID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new RoomUnavailableDatesDTO(

                                                         RoomUnavailableDateID: (int)reader["RoomUnavailableDateID"],
                             RoomID: (int)reader["RoomID"],
                             CheckIn: (DateTime)reader["CheckIn"],
                             CheckOut: (DateTime)reader["CheckOut"],
                             Reason: (string)reader["Reason"],
                             IsActive: (bool)reader["IsActive"]
    

                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }


        public static bool UpdateRoomUnavailableDates(RoomUnavailableDatesDTO roomunavailabledates)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateRoomUnavailableDatesByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomUnavailableDateID", roomunavailabledates.RoomUnavailableDateID);
                        command.Parameters.AddWithValue("@RoomID", roomunavailabledates.RoomID);
                        command.Parameters.AddWithValue("@CheckIn", roomunavailabledates.CheckIn);
                        command.Parameters.AddWithValue("@CheckOut", roomunavailabledates.CheckOut);
                        command.Parameters.AddWithValue("@Reason", roomunavailabledates.Reason);
                        command.Parameters.AddWithValue("@IsActive", roomunavailabledates.IsActive);
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static bool DeleteRoomUnavailableDates(int RoomUnavailableDateID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteRoomUnavailableDates", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomUnavailableDateID", RoomUnavailableDateID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }



        public static async Task<bool> IsRoomAvailableBetweenDates(int RoomID, DateTime CheckIn, DateTime CheckOut)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT dbo.FN_IsRoomAvailableBetweenDates(@RoomID, @NewCheckIn, @NewCheckOut)";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@RoomID", RoomID);
                        command.Parameters.AddWithValue("@NewCheckIn", CheckIn);
                        command.Parameters.AddWithValue("@NewCheckOut", CheckOut);

                        object result = await command.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToBoolean(result); // 🔥 safest way for SQL BIT
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optional: log exception
                }

                return false; // Default to false if something went wrong
            }
        }



    }
}


