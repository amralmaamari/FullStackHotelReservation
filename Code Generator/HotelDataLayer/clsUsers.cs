
using System;
using System.Data;
using Microsoft.Data.SqlClient;


namespace HotelDataAccessLayer
{

    public class UsersDTO
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


        public UsersDTO(int UserID, string Username, string Email, string Password, int CountryID, string Image, string City, bool IsAdmin, DateTime CreatedAt, DateTime UpdatedAt)
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

        public static List<UsersDTO> GetAllUsers()
        {

            List<UsersDTO> usersList = new List<UsersDTO>();
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
                                var users = new UsersDTO(
                                                             UserID: (int)reader["UserID"],
                             Username: (string)reader["Username"],
                             Email: (string)reader["Email"],
                             Password: (string)reader["Password"],
                             CountryID: (int)reader["CountryID"],
                             Image: (string)reader["Image"],
                             City: (string)reader["City"],
                             IsAdmin: (bool)reader["IsAdmin"],
                             CreatedAt: (DateTime)reader["CreatedAt"],
                             UpdatedAt: (DateTime)reader["UpdatedAt"],
    

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


        public static Nullable<int> AddNewUsers(UsersDTO users)
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
                        ;
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


        public static UsersDTO GetUsersInfoByID(int UserID)
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
                                return new UsersDTO(

                                                         UserID: (int)reader["UserID"],
                             Username: (string)reader["Username"],
                             Email: (string)reader["Email"],
                             Password: (string)reader["Password"],
                             CountryID: (int)reader["CountryID"],
                             Image: (string)reader["Image"],
                             City: (string)reader["City"],
                             IsAdmin: (bool)reader["IsAdmin"],
                             CreatedAt: (DateTime)reader["CreatedAt"],
                             UpdatedAt: (DateTime)reader["UpdatedAt"],
    

                                );

                            }
                        }

                    }
                }
                catch (Exception ex) { }

                return null;
            }


        }


        public static bool UpdateUsers(UsersDTO users)
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
                        ;
                        rowAffected = command.ExecuteNonQuery();



                    }
                }
                catch (Exception ex) { }

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


    }
}


