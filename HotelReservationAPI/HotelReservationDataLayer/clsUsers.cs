
using System;
using System.Data;
using HotelReservationAPI.Model;
using HotelReservationDataLayer;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;


namespace HotelDataAccessLayer
{

    public class UserDTO
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CountryID { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public UserDTO(int UserID, string Username, string Email, string Password, int CountryID, string Image, string City, bool IsAdmin, DateTime CreatedAt, DateTime UpdatedAt)
        {
            this.UserID = UserID;
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.CountryID = CountryID;
            this.Image = Image;
            this.City = City;
            this.IsAdmin = IsAdmin;
            this.CreatedAt = CreatedAt;
            this.UpdatedAt = UpdatedAt;
        }
    }


    public class clsUsersData
    {

        public static List<UserDTO> GetAllUsers()
        {

            List<UserDTO> usersList = new List<UserDTO>();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string Query = "select * From FN_GetAllUsers()";
                try
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                var users = new UserDTO(
                                 UserID: (int)reader["UserID"],
                                 Username: (string)reader["Username"],
                                 Email: (string)reader["Email"],
                                 Password: (string)reader["Password"],
                                 CountryID: reader["CountryID"] == DBNull.Value ? 0 : (int)reader["CountryID"],
                                 Image: reader["Image"] == DBNull.Value ? null : (string)reader["Image"],
                                 City: reader["City"] == DBNull.Value ? null : (string)reader["City"],
                                 IsAdmin: (bool)reader["IsAdmin"],
                                 CreatedAt: (DateTime)reader["CreatedAt"],
                                 UpdatedAt: (DateTime)reader["UpdatedAt"]

                                    );

                                usersList.Add(users);
                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return usersList;
            }


        }


        public static Nullable<int> AddNewUsers(UserDTO users)
        {

            Nullable<int> NewUsersID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Username", users.Username);
                        command.Parameters.AddWithValue("@Email", users.Email);
                        command.Parameters.AddWithValue("@Password", users.Password);
                        command.Parameters.AddWithValue("@CountryID", users.CountryID);
                        command.Parameters.AddWithValue("@Image", users.Image);
                        command.Parameters.AddWithValue("@City", users.City);
                        command.Parameters.AddWithValue("@IsAdmin", users.IsAdmin);
                        command.Parameters.AddWithValue("@CreatedAt", users.CreatedAt);
                        command.Parameters.AddWithValue("@UpdatedAt", users.UpdatedAt);
                        
                        SqlParameter outputIdParam = new SqlParameter("@UserID", SqlDbType.Int);
                        {
                            outputIdParam.Direction = ParameterDirection.Output;
                        }
                        ;
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            NewUsersID = Convert.ToInt32(outputIdParam.Value);
                        }

                    }
                }
                catch (Exception ex) { }

                return NewUsersID;
            }


        }


        public static UserDTO GetUsersInfoByID(int UserID)
        {

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();



                try
                {
                    using (SqlCommand command = new SqlCommand("SP_GetUsersInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new UserDTO(

                                UserID: (int)reader["UserID"],
                                Username: (string)reader["Username"],
                                Email: (string)reader["Email"],
                                Password: (string)reader["Password"],
                                CountryID: reader["CountryID"] == DBNull.Value ? 0 : (int)reader["CountryID"],
                                Image: reader["Image"] == DBNull.Value ? null : (string)reader["Image"],
                                City: reader["City"] == DBNull.Value ? null : (string)reader["City"],
                                IsAdmin: (bool)reader["IsAdmin"],
                                CreatedAt: (DateTime)reader["CreatedAt"],
                                UpdatedAt: (DateTime)reader["UpdatedAt"]



                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }


        public static bool UpdateUsers(UserDTO users)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateUsersByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", users.UserID);
                        command.Parameters.AddWithValue("@Username", users.Username);
                        command.Parameters.AddWithValue("@Email", users.Email);
                        command.Parameters.AddWithValue("@Password", users.Password);
                        command.Parameters.AddWithValue("@CountryID", users.CountryID);
                        command.Parameters.AddWithValue("@Image", users.Image);
                        command.Parameters.AddWithValue("@City", users.City);
                        command.Parameters.AddWithValue("@IsAdmin", users.IsAdmin);
                        command.Parameters.AddWithValue("@CreatedAt", users.CreatedAt);
                        command.Parameters.AddWithValue("@UpdatedAt", users.UpdatedAt);
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                // When SET NOCOUNT ON is enabled, ExecuteNonQuery returns -1, which indicates success.

                return (rowAffected != 0);
            }


        }


        public static bool DeleteUsers(int UserID)
        {

            Nullable<int> rowAffected = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@UserID", UserID);

                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

                return (rowAffected != 0);
            }


        }



        public static async Task<bool> IsUserExists(LoginDTO loginDTO)
        {
            //this funcation has two parmeter and return bool 
            bool userExists = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                await connection.OpenAsync();


                try
                {
                    using (SqlCommand command = new SqlCommand("SP_IsUserExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", loginDTO.Email);
                        command.Parameters.AddWithValue("@Password", loginDTO.Password);

                        // Execute the stored procedure and get the result set
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Retrieve the value from the "UserExists" column
                                userExists = reader.GetBoolean(reader.GetOrdinal("UserExists"));
                            }
                        }


                    }
                }
                catch (SqlException ex)
                {
                    // Log or handle specific SqlException if necessary
                    Console.WriteLine("SQL Error: " + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    // Log or handle generic exceptions
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }


            }
            return userExists;
        }

    }
}


