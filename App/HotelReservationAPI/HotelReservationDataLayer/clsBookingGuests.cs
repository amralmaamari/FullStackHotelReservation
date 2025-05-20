
using System;
using System.Data;
using HotelReservationDataLayer;
using Microsoft.Data.SqlClient;


namespace HotelDataAccessLayer
{

    public class BookingGuestsDTO
    {
        public int BookingGuestsID { get; set; }
        public int BookingID { get; set; }
        public int GuestID { get; set; }


        public BookingGuestsDTO(int BookingGuestsID, int BookingID, int GuestID)
        {
            this.BookingGuestsID = BookingGuestsID;
            this.BookingID = BookingID;
            this.GuestID = GuestID;
        }
    }


    public class clsBookingGuestsData
    {

        public static List<BookingGuestsDTO> GetAllBookingGuests()
        {

            List<BookingGuestsDTO> bookingguestsList = new List<BookingGuestsDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllBookingGuests()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var bookingguests = new BookingGuestsDTO(
                                                             BookingGuestsID: (int)reader["BookingGuestsID"],
                             BookingID: (int)reader["BookingID"],
                             GuestID: (int)reader["GuestID"]
    

                                );

                                bookingguestsList.Add(bookingguests);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return bookingguestsList;
            }


        }


        public static Nullable<int> AddNewBookingGuests(BookingGuestsDTO bookingguests)
        {

            Nullable<int> NewBookingGuestsID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewBookingGuests", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BookingID", bookingguests.BookingID);
                        command.Parameters.AddWithValue("@GuestID", bookingguests.GuestID);
                        ;
                        SqlParameter outputIdParam = new SqlParameter("@BookingGuestsID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewBookingGuestsID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewBookingGuestsID;
            }


        }


        public static BookingGuestsDTO GetBookingGuestsInfoByID(int BookingGuestsID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBookingGuestsInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingGuestsID", BookingGuestsID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new BookingGuestsDTO(

                                                         BookingGuestsID: (int)reader["BookingGuestsID"],
                             BookingID: (int)reader["BookingID"],
                             GuestID: (int)reader["GuestID"]
    

                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }

        public static BookingGuestsDTO GetBookingGuestsInfoByBookingID(int BookingID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBookingGuestsInfoByBookingID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingID", BookingID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new BookingGuestsDTO(

                             BookingGuestsID: (int)reader["BookingGuestsID"],
                             BookingID: (int)reader["BookingID"],
                             GuestID: (int)reader["GuestID"]


                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }
        public static bool UpdateBookingGuests(BookingGuestsDTO bookingguests)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateBookingGuestsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingGuestsID", bookingguests.BookingGuestsID);
                        command.Parameters.AddWithValue("@BookingID", bookingguests.BookingID);
                        command.Parameters.AddWithValue("@GuestID", bookingguests.GuestID);
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static bool DeleteBookingGuests(int BookingGuestsID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteBookingGuests", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingGuestsID", BookingGuestsID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


    }
}


