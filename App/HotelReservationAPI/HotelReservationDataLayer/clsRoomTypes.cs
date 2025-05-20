
using System;
using System.Data;
using HotelReservationDataLayer;
using Microsoft.Data.SqlClient;


namespace HotelDataAccessLayer
{

    public class RoomTypesDTO
    {
        public int RoomTypeID { get; set; }
        public string Title { get; set; }
        public int MaxPeople { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }


        public RoomTypesDTO(int RoomTypeID, string Title, int MaxPeople, string Description, DateTime CreatedAt, DateTime UpdateAt)
        {
            this.RoomTypeID = RoomTypeID;
            this.Title = Title;
            this.MaxPeople = MaxPeople;
            this.Description = Description;
            this.CreatedAt = CreatedAt;
            this.UpdateAt = UpdateAt;
        }
    }


    public class clsRoomTypesData
    {

        public static List<RoomTypesDTO> GetAllRoomTypes()
        {

            List<RoomTypesDTO> roomtypesList = new List<RoomTypesDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllRoomTypes()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var roomtypes = new RoomTypesDTO(
                                                             RoomTypeID: (int)reader["RoomTypeID"],
                             Title: (string)reader["Title"],
                             MaxPeople: (int)reader["MaxPeople"],
                             Description: (string)reader["Description"],
                             CreatedAt: (DateTime)reader["CreatedAt"],
                             UpdateAt: (DateTime)reader["UpdateAt"]
    

                                );

                                roomtypesList.Add(roomtypes);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return roomtypesList;
            }


        }


        public static Nullable<int> AddNewRoomTypes(RoomTypesDTO roomtypes)
        {

            Nullable<int> NewRoomTypesID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewRoomTypes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Title", roomtypes.Title);
                        command.Parameters.AddWithValue("@MaxPeople", roomtypes.MaxPeople);
                        command.Parameters.AddWithValue("@Description", roomtypes.Description);
                        command.Parameters.AddWithValue("@CreatedAt", roomtypes.CreatedAt);
                        command.Parameters.AddWithValue("@UpdateAt", roomtypes.UpdateAt);
                        ;
                        SqlParameter outputIdParam = new SqlParameter("@RoomTypeID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewRoomTypesID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewRoomTypesID;
            }


        }


        public static RoomTypesDTO GetRoomTypesInfoByID(int RoomTypeID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetRoomTypesInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomTypeID", RoomTypeID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new RoomTypesDTO(

                               RoomTypeID: (int)reader["RoomTypeID"],
                             Title: (string)reader["Title"],
                             MaxPeople: (int)reader["MaxPeople"],
                             Description: (string)reader["Description"],
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


        public static bool UpdateRoomTypes(RoomTypesDTO roomtypes)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateRoomTypesByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomTypeID", roomtypes.RoomTypeID);
                        command.Parameters.AddWithValue("@Title", roomtypes.Title);
                        command.Parameters.AddWithValue("@MaxPeople", roomtypes.MaxPeople);
                        command.Parameters.AddWithValue("@Description", roomtypes.Description);
                        command.Parameters.AddWithValue("@CreatedAt", roomtypes.CreatedAt);
                        command.Parameters.AddWithValue("@UpdateAt", roomtypes.UpdateAt);
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static bool DeleteRoomTypes(int RoomTypeID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteRoomTypes", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomTypeID", RoomTypeID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


    }
}


