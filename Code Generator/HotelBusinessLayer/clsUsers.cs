
using System;
using System.Data;
using System.Data.SqlClient;

namespace Hotel_Business
{
    public class clsUsers
    {


        public enum enMode { AddNew = 0, Update = 1 }
        public static enMode Mode = enMode.AddNew;

        public UsersDTO usersDTO
        {
            get
            {
                return new UsersDTO(
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

        public clsUsers(UsersDTO users, enMode mode = enMode.AddNew)
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
        public static List<UsersDTO> GetAllUsers()
        {
            return clsUsersData.GetAllUsers();

        }


        public static clsUsers GetUsersInfoByID(int UserID)
        {
            UsersDTO usersDTO = clsUsersData.GetUsersInfoByID(UserID);

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

            this.UserID = (int)clsUsersData.AddNewUsers(this.usersDTO);
            return (this.UserID != -1);

        }

        private bool _UpdateUsers()
        {

            return (clsUsersData.UpdateUsers(this.usersDTO));
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



    }
}


