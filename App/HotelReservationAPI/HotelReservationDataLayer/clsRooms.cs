
using System;
using System.Data;
using Newtonsoft.Json;
using HotelReservationDataLayer;
using HotelReservationDataLayer.Model;
using Microsoft.Data.SqlClient;


namespace HotelDataAccessLayer
{

    public class RoomDTO
    {
        public int RoomID { get; set; }
        public int RoomTypeID { get; set; }
        public int HotelID { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }


        public RoomDTO(int RoomID, int RoomTypeID, int HotelID, string RoomNumber, decimal Price, DateTime CreatedAt, DateTime UpdateAt)
        {
            this.RoomID = RoomID;
            this.RoomTypeID = RoomTypeID;
            this.HotelID = HotelID;
            this.RoomNumber = RoomNumber;
            this.Price = Price;
            this.CreatedAt = CreatedAt;
            this.UpdateAt = UpdateAt;
        }
    }


    public class clsRoomsData
    {

        public static List<RoomDTO> GetAllRooms()
        {

            List<RoomDTO> roomsList = new List<RoomDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllRooms()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var rooms = new RoomDTO(
                                                             RoomID: (int)reader["RoomID"],
                             RoomTypeID: (int)reader["RoomTypeID"],
                             HotelID: (int)reader["HotelID"],
                             RoomNumber: (string)reader["RoomNumber"],
                             Price: (decimal)reader["Price"],
                             CreatedAt: (DateTime)reader["CreatedAt"],
                             UpdateAt: (DateTime)reader["UpdateAt"]


                                );

                                roomsList.Add(rooms);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return roomsList;
            }


        }


        public static Nullable<int> AddNewRooms(RoomDTO rooms)
        {

            Nullable<int> NewRoomsID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewRooms", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomTypeID", rooms.RoomTypeID);
                        command.Parameters.AddWithValue("@HotelID", rooms.HotelID);
                        command.Parameters.AddWithValue("@RoomNumber", rooms.RoomNumber);
                        command.Parameters.AddWithValue("@Price", rooms.Price);
                        command.Parameters.AddWithValue("@CreatedAt", rooms.CreatedAt);
                        command.Parameters.AddWithValue("@UpdateAt", rooms.UpdateAt);
                        ;
                        SqlParameter outputIdParam = new SqlParameter("@RoomID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewRoomsID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewRoomsID;
            }


        }


        public static RoomDTO GetRoomsInfoByID(int RoomID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetRoomsInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomID", RoomID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new RoomDTO(

                                                         RoomID: (int)reader["RoomID"],
                                                         RoomTypeID: (int)reader["RoomTypeID"],
                                                         HotelID: (int)reader["HotelID"],
                                                         RoomNumber: (string)reader["RoomNumber"],
                                                         Price: (decimal)reader["Price"],
                                                         CreatedAt: (DateTime)reader["CreatedAt"],
                                                         UpdateAt: (DateTime)reader["UpdateAt"]


                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }


        public static bool UpdateRooms(RoomDTO rooms)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateRoomsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomID", rooms.RoomID);
                        command.Parameters.AddWithValue("@RoomTypeID", rooms.RoomTypeID);
                        command.Parameters.AddWithValue("@HotelID", rooms.HotelID);
                        command.Parameters.AddWithValue("@RoomNumber", rooms.RoomNumber);
                        command.Parameters.AddWithValue("@Price", rooms.Price);
                        command.Parameters.AddWithValue("@CreatedAt", rooms.CreatedAt);
                        command.Parameters.AddWithValue("@UpdateAt", rooms.UpdateAt);
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static bool DeleteRooms(int RoomID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteRooms", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomID", RoomID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }



        public static List<RoomReserveDTO> GetAllRoomsOfHotelID(int HotelID)
        {
            List<RoomReserveDTO> roomsList = new List<RoomReserveDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM FN_GetAllRoomsOfHotelID(@HotelID)";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@HotelID", HotelID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var roomNumbersJson = reader["RoomNumbers"].ToString();
                                var roomNumbers = JsonConvert.DeserializeObject<List<RoomReserveDTO.RoomNumberDTO>>(roomNumbersJson);

                                var room = new RoomReserveDTO
                                (
                                    HotelID: reader.GetInt32(reader.GetOrdinal("HotelID")),
                                    Price: reader.GetDecimal(reader.GetOrdinal("Price")),
                                    RoomTypeID: reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                                    Title: reader.GetString(reader.GetOrdinal("Title")),
                                    MaxPeople: reader.GetInt32(reader.GetOrdinal("MaxPeople")),
                                    Description: reader.GetString(reader.GetOrdinal("Description")),
                                    RoomNumbers: roomNumbers
                                );

                                roomsList.Add(room);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in GetAllRoomsOfHotelID: " + ex.Message);
                    // Consider rethrowing the exception or handling it as needed
                    throw;  // Rethrowing the exception preserves the original stack trace
                }

                return roomsList;
            }
        }


    
    }
}


