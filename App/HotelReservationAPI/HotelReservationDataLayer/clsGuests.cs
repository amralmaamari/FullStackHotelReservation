
using System;
using System.Data;
using HotelReservationDataLayer;
using HotelReservationDataLayer.Model;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;


namespace HotelDataAccessLayer
{

    public class GuestsDTO
    {
        public int GuestID { get; set; }
        public string IDCardNumber { get; set; }
        public string FullName { get; set; }


        public GuestsDTO(int GuestID, string IDCardNumber, string FullName)
        {
            this.GuestID = GuestID;
            this.IDCardNumber = IDCardNumber;
            this.FullName = FullName;
        }
    }


    public class clsGuestsData
    {

        public static List<GuestsDTO> GetAllGuests()
        {

            List<GuestsDTO> guestsList = new List<GuestsDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllGuests()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var guests = new GuestsDTO(
                                                             GuestID: (int)reader["GuestID"],
                             IDCardNumber: (string)reader["IDCardNumber"],
                             FullName: (string)reader["FullName"]


                                );

                                guestsList.Add(guests);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return guestsList;
            }


        }
        public static List<GuestsDTO> GetAllGuestsByBookingID(int bookingID)
        {
            var guestsList = new List<GuestsDTO>();

            using (var connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM FN_GetAllGuestsOfBookingID(@BookingID)";

                try
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("@BookingID", SqlDbType.Int).Value = bookingID;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var guest = new GuestsDTO(
                                    GuestID: reader.GetInt32(reader.GetOrdinal("GuestID")),
                                    IDCardNumber: reader.GetString(reader.GetOrdinal("IDCardNumber")),
                                    FullName: reader.GetString(reader.GetOrdinal("FullName"))
                                );
                                guestsList.Add(guest);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error (preferably to file/service in real app)
                    Console.WriteLine("Error in GetAllGuestsByBookingID: " + ex.Message);
                    throw;
                }
            }

            return guestsList;
        }

        public static Nullable<int> AddNewGuests(GuestsDTO guests)
        {

            Nullable<int> NewGuestsID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewGuests", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IDCardNumber", guests.IDCardNumber);
                        command.Parameters.AddWithValue("@FullName", guests.FullName);
                        ;
                        SqlParameter outputIdParam = new SqlParameter("@GuestID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewGuestsID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewGuestsID;
            }


        }


        public static GuestsDTO GetGuestsInfoByID(int GuestID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetGuestsInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GuestID", GuestID);

                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new GuestsDTO(

                                                         GuestID: (int)reader["GuestID"],
                             IDCardNumber: (string)reader["IDCardNumber"],
                             FullName: (string)reader["FullName"]


                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }


        public static bool UpdateGuests(GuestsDTO guests)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateGuestsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GuestID", guests.GuestID);
                        command.Parameters.AddWithValue("@IDCardNumber", guests.IDCardNumber);
                        command.Parameters.AddWithValue("@FullName", guests.FullName);
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static bool DeleteGuests(int GuestID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteGuests", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GuestID", GuestID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


    }
}


