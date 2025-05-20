
using System;
using System.Data;
using System.Data.SqlClient;
using HotelDataAccessLayer;

namespace Hotel_Business
{
    public class clsRoomUnavailableDates
    {


        public enum enMode { AddNew = 0, Update = 1 }
        public static enMode Mode = enMode.AddNew;

        public RoomUnavailableDatesDTO roomunavailabledatesDTO
        {
            get
            {
                return new RoomUnavailableDatesDTO(
              this.RoomUnavailableDateID,
              this.RoomID,
              this.CheckIn,
              this.CheckOut,
              this.Reason,
              this.IsActive

               );
            }
        }

        public int RoomUnavailableDateID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }


        public clsRoomUnavailableDates()
        {
            this.RoomUnavailableDateID = -1;

            this.RoomID = -1;

            this.CheckIn = DateTime.Now.Date;

            this.CheckOut = DateTime.Now.Date;

            this.Reason = "";

            this.IsActive = false;

            Mode = enMode.AddNew;
        }

        public clsRoomUnavailableDates(RoomUnavailableDatesDTO roomunavailabledates, enMode mode = enMode.AddNew)
        {

            this.RoomUnavailableDateID = roomunavailabledates.RoomUnavailableDateID;

            this.RoomID = roomunavailabledates.RoomID;

            this.CheckIn = roomunavailabledates.CheckIn;

            this.CheckOut = roomunavailabledates.CheckOut;

            this.Reason = roomunavailabledates.Reason;

            this.IsActive = roomunavailabledates.IsActive;

            Mode = mode;
        }
        public static List<RoomUnavailableDatesDTO> GetAllRoomUnavailableDates()
        {
            return clsRoomUnavailableDatesData.GetAllRoomUnavailableDates();

        }


        public static clsRoomUnavailableDates GetRoomUnavailableDatesInfoByID(int RoomUnavailableDateID)
        {
            RoomUnavailableDatesDTO roomunavailabledatesDTO = clsRoomUnavailableDatesData.GetRoomUnavailableDatesInfoByID(RoomUnavailableDateID);

            if (roomunavailabledatesDTO != null)
            {
                return new clsRoomUnavailableDates(roomunavailabledatesDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewRoomUnavailableDates()
        {

            this.RoomUnavailableDateID = (int)clsRoomUnavailableDatesData.AddNewRoomUnavailableDates(this.roomunavailabledatesDTO);
            return (this.RoomUnavailableDateID != -1);

        }

        private bool _UpdateRoomUnavailableDates()
        {

            return (clsRoomUnavailableDatesData.UpdateRoomUnavailableDates(this.roomunavailabledatesDTO));
        }

        public bool Save()
        {

            if (Mode == enMode.AddNew)
            {
                if (_AddNewRoomUnavailableDates())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return _UpdateRoomUnavailableDates();
            }

        }

        public static bool DeleteRoomUnavailableDates(int RoomUnavailableDateID)
        {
            return clsRoomUnavailableDatesData.DeleteRoomUnavailableDates(RoomUnavailableDateID);

        }
        public static bool DeactivateRoomUnavailableByBooking(int BookingID)
        {
            return clsRoomUnavailableDatesData.DeactivateRoomUnavailableByBooking(BookingID);
        }

        public static async Task<bool> IsRoomAvailableBetweenDates(int RoomID, DateTime CheckIn, DateTime CheckOut)
        {
            return await clsRoomUnavailableDatesData.IsRoomAvailableBetweenDates(RoomID, CheckIn, CheckOut);
        }


    }
}


