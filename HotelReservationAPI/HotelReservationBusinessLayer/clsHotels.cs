
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HotelDataAccessLayer;
using HotelReservationAPI.Model;
using HotelReservationDataLayer.Model;

namespace Hotel_Business
{
    public class clsHotels
    {


       public enum enMode { AddNew = 0, Update = 1 }
       public static enMode Mode = enMode.AddNew;

        public HotelDTO hotelsDTO
        {
            get
            {
                return new HotelDTO(
              this.HotelID,
              this.Name,
              this.HotelTypeID,
              this.City,
              this.Address,
              this.Distance,
              this.Title,
              this.Description,
              this.Rating,
              this.Featured,
              this.CreatedAt,
              this.UpdateAt
        
               );
            }
        }

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


        public clsHotels()
        {
            this.HotelID = -1;

            this.Name = "";

            this.HotelTypeID = -1;

            this.City = "";

            this.Address = "";

            this.Distance = "";

            this.Title = "";

            this.Description = "";

            this.Rating = 0m;

            this.Featured = false;

            this.CreatedAt = DateTime.Now;

            this.UpdateAt = DateTime.Now;

            Mode = enMode.AddNew;
        }

        public clsHotels(HotelDTO hotels, enMode mode = enMode.AddNew)
        {

            this.HotelID = hotels.HotelID;

            this.Name = hotels.Name;

            this.HotelTypeID = hotels.HotelTypeID;

            this.City = hotels.City;

            this.Address = hotels.Address;

            this.Distance = hotels.Distance;

            this.Title = hotels.Title;

            this.Description = hotels.Description;

            this.Rating = hotels.Rating;

            this.Featured = hotels.Featured;

            this.CreatedAt = hotels.CreatedAt;

            this.UpdateAt = hotels.UpdateAt;

            Mode = mode;
        }
        public static List<HotelDTO> GetAllHotels()
        {
            return clsHotelsData.GetAllHotels();

        }


        public static clsHotels GetHotelsInfoByID(int HotelID)
        {
            HotelDTO hotelsDTO = clsHotelsData.GetHotelsInfoByID(HotelID);

            if (hotelsDTO != null)
            {
                return new clsHotels(hotelsDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        public static HotelDetailsDTO GetHotelDetailsByID(int HotelID)
        {
            var hotelsDetailsDTO = clsHotelsData.GetHotelsDetailsByID(HotelID);
            if (hotelsDetailsDTO != null)
            {
                return hotelsDetailsDTO;
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewHotels()
        {

            this.HotelID = (int)clsHotelsData.AddNewHotels(this.hotelsDTO);
            return (this.HotelID != -1);

        }
        
        private bool _UpdateHotels()
        {

            return (clsHotelsData.UpdateHotels(this.hotelsDTO));
        }

        public bool Save()
        {

            if (Mode == enMode.AddNew)
            {
                if (_AddNewHotels())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return _UpdateHotels();
            }

        }

        public static bool DeleteHotels(int HotelID)
        {
            return clsHotelsData.DeleteHotels(HotelID);

        }


        public static async Task<bool> IsUserExists(LoginDTO loginDTO)
        {
            return await clsUsersData.IsUserExists(loginDTO);

        }


        public static List<HotelDetailsDTO> GetAllHotelsDetailsParameters( int pageSize, decimal minPrice, decimal maxPrice)
        {
            return clsHotelsData.GetAllHotelsDetailsParameters(pageSize, minPrice, maxPrice); 
        }

        public static async Task<int> CountHotelsByCity(string city)
        {
            return await clsHotelsData.CountHotelsByCity(city);
        }

        public static async Task<int> CountHotelsByType(string type)
        {
            return await clsHotelsData.CountHotelsByType(type);

        }
    }
}


