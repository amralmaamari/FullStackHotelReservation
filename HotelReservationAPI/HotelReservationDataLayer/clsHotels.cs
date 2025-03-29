
using System;
using System.Data;
using HotelReservationDataLayer;
using HotelReservationDataLayer.Model;
using Microsoft.Data.SqlClient;


namespace HotelDataAccessLayer
{

    public class HotelDTO
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public int HotelTypeID { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Distance { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public bool Featured { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }


        public HotelDTO(int HotelID, string Name, int HotelTypeID, string City, string Address, string Distance, string Title, string Description, decimal Rating, bool Featured, DateTime CreatedAt, DateTime UpdateAt)
        {
            this.HotelID = HotelID;
            this.Name = Name;
            this.HotelTypeID = HotelTypeID;
            this.City = City;
            this.Address = Address;
            this.Distance = Distance;
            this.Title = Title;
            this.Description = Description;
            this.Rating = Rating;
            this.Featured = Featured;
            this.CreatedAt = CreatedAt;
            this.UpdateAt = UpdateAt;
        }
    }


    public class clsHotelsData
    {

        public static List<HotelDTO> GetAllHotels()
        {

            List<HotelDTO> hotelsList = new List<HotelDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllHotels()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var hotels = new HotelDTO(
                                                             HotelID: (int)reader["HotelID"],
                             Name: (string)reader["Name"],
                             HotelTypeID: (int)reader["HotelTypeID"],
                             City: (string)reader["City"],
                             Address: (string)reader["Address"],
                             Distance: (string)reader["Distance"],
                             Title: (string)reader["Title"],
                             Description: (string)reader["Description"],
                             Rating: (decimal)reader["Rating"],
                             Featured: (bool)reader["Featured"],
                             CreatedAt: (DateTime)reader["CreatedAt"],
                             UpdateAt: (DateTime)reader["UpdateAt"]


                                );

                                hotelsList.Add(hotels);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return hotelsList;
            }


        }


        public static Nullable<int> AddNewHotels(HotelDTO hotels)
        {

            Nullable<int> NewHotelsID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewHotels", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Name", hotels.Name);
                        command.Parameters.AddWithValue("@HotelTypeID", hotels.HotelTypeID);
                        command.Parameters.AddWithValue("@City", hotels.City);
                        command.Parameters.AddWithValue("@Address", hotels.Address);
                        command.Parameters.AddWithValue("@Distance", hotels.Distance);
                        command.Parameters.AddWithValue("@Title", hotels.Title);
                        command.Parameters.AddWithValue("@Description", hotels.Description);
                        command.Parameters.AddWithValue("@Rating", hotels.Rating);
                        command.Parameters.AddWithValue("@Featured", hotels.Featured);
                        command.Parameters.AddWithValue("@CreatedAt", hotels.CreatedAt);
                        command.Parameters.AddWithValue("@UpdateAt", hotels.UpdateAt);
                        ;
                        SqlParameter outputIdParam = new SqlParameter("@HotelID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewHotelsID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewHotelsID;
            }


        }


        public static HotelDTO GetHotelsInfoByID(int HotelID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetHotelsInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@HotelID", HotelID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new HotelDTO(

                                                         HotelID: (int)reader["HotelID"],
                             Name: (string)reader["Name"],
                             HotelTypeID: (int)reader["HotelTypeID"],
                             City: (string)reader["City"],
                             Address: (string)reader["Address"],
                             Distance: (string)reader["Distance"],
                             Title: (string)reader["Title"],
                             Description: (string)reader["Description"],
                             Rating: (decimal)reader["Rating"],
                             Featured: (bool)reader["Featured"],
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


        public static HotelDetailsDTO GetHotelsDetailsByID(int HotelID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetHotelDetailsByHotelID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@HotelID", HotelID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new HotelDetailsDTO(
                                    HotelID: (int)reader["HotelID"],
                                    TypeName: reader["TypeName"].ToString(),
                                    Name: reader["Name"].ToString(),
                                    City: reader["City"].ToString(),
                                    Address: reader["Address"].ToString(),
                                    Distance: reader["Distance"]?.ToString(),
                                    Photos: reader["Photos"]?.ToString(),
                                    Rooms: reader["Rooms"]?.ToString(),
                                    Title: reader["Title"].ToString(),
                                    Description: reader["Description"].ToString(),
                                    CheapestPrice: reader["CheapestPrice"] == DBNull.Value ? null : (decimal?)reader["CheapestPrice"],
                                    Featured: (bool)reader["Featured"]
                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }


        public static bool UpdateHotels(HotelDTO hotels)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateHotelsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@HotelID", hotels.HotelID);
                        command.Parameters.AddWithValue("@Name", hotels.Name);
                        command.Parameters.AddWithValue("@HotelTypeID", hotels.HotelTypeID);
                        command.Parameters.AddWithValue("@City", hotels.City);
                        command.Parameters.AddWithValue("@Address", hotels.Address);
                        command.Parameters.AddWithValue("@Distance", hotels.Distance);
                        command.Parameters.AddWithValue("@Title", hotels.Title);
                        command.Parameters.AddWithValue("@Description", hotels.Description);
                        command.Parameters.AddWithValue("@Rating", hotels.Rating);
                        command.Parameters.AddWithValue("@Featured", hotels.Featured);
                        command.Parameters.AddWithValue("@CreatedAt", hotels.CreatedAt);
                        command.Parameters.AddWithValue("@UpdateAt", hotels.UpdateAt);
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static bool DeleteHotels(int HotelID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteHotels", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@HotelID", HotelID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }


        public static List<HotelDetailsDTO> GetAllHotelsDetailsParameters(int pageSize, decimal minPrice, decimal maxPrice)
        {
            var hotelsDetailsList = new List<HotelDetailsDTO>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string query = @"SELECT * FROM FN_GetAllHotelsDetailsParameters(@Page, @PageSize, @MinPrice, @MaxPrice)";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        // Add parameters safely
                        command.Parameters.AddWithValue("@Page", 1);
                        command.Parameters.AddWithValue("@PageSize", pageSize);
                        command.Parameters.AddWithValue("@MinPrice", minPrice);
                        command.Parameters.AddWithValue("@MaxPrice", maxPrice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var hotel = new HotelDetailsDTO(
                                    HotelID: (int)reader["HotelID"],
                                    TypeName: reader["TypeName"].ToString(),
                                    Name: reader["Name"].ToString(),
                                    City: reader["City"].ToString(),
                                    Address: reader["Address"].ToString(),
                                    Distance: reader["Distance"]?.ToString(),
                                    Photos: reader["Photos"]?.ToString(),
                                    Rooms: reader["Rooms"]?.ToString(),
                                    Title: reader["Title"].ToString(),
                                    Description: reader["Description"].ToString(),
                                    CheapestPrice: reader["CheapestPrice"] == DBNull.Value ? null : (decimal?)reader["CheapestPrice"],
                                    Featured: (bool)reader["Featured"]
                                );

                                hotelsDetailsList.Add(hotel);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optional: Log the exception
                    Console.WriteLine("Error fetching hotel details: " + ex.Message);
                }

                return hotelsDetailsList;
            }
        }


        public static async Task<int> CountHotelsByCity(string city)
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT dbo.FN_GetHotelCountByCity(@City)";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@City", city);

                        object result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int parsedCount))
                        {
                            count = parsedCount;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the error if necessary
                }

                return count;
            }
        }


        public static async Task<int> CountHotelsByType(string type)
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT dbo.FN_TotalHotelsByType(@TypeName)";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@TypeName", type);

                        object result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int parsedCount))
                        {
                            count = parsedCount;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the error if necessary
                }

                return count;
            }
        }
    }
}


