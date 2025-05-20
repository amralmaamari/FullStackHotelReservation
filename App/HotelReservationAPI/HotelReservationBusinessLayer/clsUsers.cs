
using System;
using System.Data;
using System.Data.SqlClient;
using HotelDataAccessLayer;
using HotelReservationAPI.Model;
using HotelReservationDataLayer.Model;

namespace Hotel_Business
{
    public class clsUsers
    {


        public enum enMode { AddNew = 0, Update = 1 }
        public static enMode Mode = enMode.AddNew;

        public UserDTO userDTO
        {
            get
            {
                return new UserDTO(
              this.UserID,
              this.Username,
              this.Email,
              this.Password,
              this.CountryID,
              this.Image,
              this.City,
              this.IsAdmin,
              this.CreatedAt,
              this.UpdatedAt
        
               );
            }
        }

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


        public clsUsers()
        {
            this.UserID = -1;

            this.Username = "";

            this.Email = "";

            this.Password = "";

            this.CountryID = -1;

            this.Image = "";

            this.City = "";

            this.IsAdmin = false;

            this.CreatedAt = DateTime.Now;

            this.UpdatedAt = DateTime.Now;

            Mode = enMode.AddNew;
        }

        public static clsUsers FromSignUpDTO(SignUpDTO dto)
        {
            return new clsUsers
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password, // Assuming HashPassword is a method to hash passwords
                CountryID = dto.CountryID ?? 1 ,
                Image = dto.Image??"ss",
                City = dto.City??"ss",
                IsAdmin = false, // Default value, could also come from DTO if needed
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
        public clsUsers(UserDTO users, enMode mode = enMode.AddNew)
        {

            this.UserID = users.UserID;

            this.Username = users.Username;

            this.Email = users.Email;

            this.Password = users.Password;

            this.CountryID = users.CountryID;

            this.Image = users.Image;

            this.City = users.City;

            this.IsAdmin = users.IsAdmin;

            this.CreatedAt = users.CreatedAt;

            this.UpdatedAt = users.UpdatedAt;

            Mode = mode;
        }
        public static List<UserDTO> GetAllUsers()
        {
            return clsUsersData.GetAllUsers();

        }


        public static clsUsers GetUsersInfoByID(int UserID)
        {
            UserDTO usersDTO = clsUsersData.GetUsersInfoByID(UserID);

            if (usersDTO != null)
            {
                return new clsUsers(usersDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewUsers()
        {

            this.UserID = (int)clsUsersData.AddNewUsers(this.userDTO);
            return (this.UserID != -1);

        }

        private bool _UpdateUsers()
        {

            return (clsUsersData.UpdateUsers(this.userDTO));
        }

        public bool Save()
        {

            if (Mode == enMode.AddNew)
            {
                if (_AddNewUsers())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return _UpdateUsers();
            }

        }

        public static bool DeleteUsers(int UserID)
        {
            return clsUsersData.DeleteUsers(UserID);

        }

        public static UserDTO GetUsersInfoByEmail(string Email)
        {
            return   clsUsersData.GetUsersInfoByEmail(Email);
        }

        public static async Task<bool> IsUserExists(LoginDTO loginDTO)
        {
            return await clsUsersData.IsUserExists(loginDTO);
        }
    }
}


