
using System;
using System.Data;
using HotelReservationDataLayer;
using Microsoft.Data.SqlClient;


namespace HotelDataAccessLayer
{

    public class BookingRoomDTO
    {
        public int BookingRoomID { get; set; }
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public decimal PaidFees { get; set; }


        public BookingRoomDTO(int BookingRoomID, int BookingID, int RoomID, decimal PaidFees)
        {
            this.BookingRoomID = BookingRoomID;
            this.BookingID = BookingID;
            this.RoomID = RoomID;
            this.PaidFees = PaidFees;
        }
    }


    public class clsBookingRoomData
    {

        public static List<BookingRoomDTO> GetAllBookingRoom()
        {

            List<BookingRoomDTO> bookingroomList = new List<BookingRoomDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllBookingRoom()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var bookingroom = new BookingRoomDTO(
                                                             BookingRoomID: (int)reader["BookingRoomID"],
                             BookingID: (int)reader["BookingID"],
                             RoomID: (int)reader["RoomID"],
                             PaidFees: (decimal)reader["PaidFees"]
    

                                );

                                bookingroomList.Add(bookingroom);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return bookingroomList;
            }


        }


        public static Nullable<int> AddNewBookingRoom(BookingRoomDTO bookingroom)
        {

            Nullable<int> NewBookingRoomID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewBookingRoom", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BookingID", bookingroom.BookingID);
                        command.Parameters.AddWithValue("@RoomID", bookingroom.RoomID);
                        command.Parameters.AddWithValue("@PaidFees", bookingroom.PaidFees);
                        ;
                        SqlParameter outputIdParam = new SqlParameter("@BookingRoomID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewBookingRoomID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewBookingRoomID;
            }


        }


        public static BookingRoomDTO GetBookingRoomInfoByID(int BookingRoomID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBookingRoomInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingRoomID", BookingRoomID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new BookingRoomDTO(

                             BookingRoomID: (int)reader["BookingRoomID"],
                             BookingID: (int)reader["BookingID"],
                             RoomID: (int)reader["RoomID"],
                             PaidFees: (decimal)reader["PaidFees"]
    

                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }


        public static bool UpdateBookingRoom(BookingRoomDTO bookingroom)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateBookingRoomByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingRoomID", bookingroom.BookingRoomID);
                        command.Parameters.AddWithValue("@BookingID", bookingroom.BookingID);
                        command.Parameters.AddWithValue("@RoomID", bookingroom.RoomID);
                        command.Parameters.AddWithValue("@PaidFees", bookingroom.PaidFees);
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static bool DeleteBookingRoom(int BookingRoomID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteBookingRoom", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingRoomID", BookingRoomID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


    }
}


