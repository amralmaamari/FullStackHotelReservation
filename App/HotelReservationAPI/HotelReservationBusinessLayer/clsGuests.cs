
using System;
using System.Data;
using System.Data.SqlClient;
using HotelDataAccessLayer;

namespace Hotel_Business
{
    public class clsGuests
    {


        public enum enMode { AddNew = 0, Update = 1 }
        public static enMode Mode = enMode.AddNew;

        public GuestsDTO guestsDTO
        {
            get
            {
                return new GuestsDTO(
              this.GuestID,
              this.IDCardNumber,
              this.FullName

               );
            }
        }

        public int GuestID { get; set; }
        public string IDCardNumber { get; set; }
        public string FullName { get; set; }


        public clsGuests()
        {
            this.GuestID = -1;

            this.IDCardNumber = "";

            this.FullName = "";

            Mode = enMode.AddNew;
        }

        public clsGuests(GuestsDTO guests, enMode mode = enMode.AddNew)
        {

            this.GuestID = guests.GuestID;

            this.IDCardNumber = guests.IDCardNumber;

            this.FullName = guests.FullName;

            Mode = mode;
        }
        public static List<GuestsDTO> GetAllGuests()
        {
            return clsGuestsData.GetAllGuests();

        }
        public static List<GuestsDTO> GetAllGuestsByBookingID(int BookingID)
        {
            return clsGuestsData.GetAllGuestsByBookingID(BookingID);
        }


        public static clsGuests GetGuestsInfoByID(int GuestID)
        {
            GuestsDTO guestsDTO = clsGuestsData.GetGuestsInfoByID(GuestID);

            if (guestsDTO != null)
            {
                return new clsGuests(guestsDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewGuests()
        {

            this.GuestID = (int)clsGuestsData.AddNewGuests(this.guestsDTO);
            return (this.GuestID != -1);

        }

        private bool _UpdateGuests()
        {

            return (clsGuestsData.UpdateGuests(this.guestsDTO));
        }

        public bool Save()
        {

            if (Mode == enMode.AddNew)
            {
                if (_AddNewGuests())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return _UpdateGuests();
            }

        }

        public static bool DeleteGuests(int GuestID)
        {
            return clsGuestsData.DeleteGuests(GuestID);

        }



    }
}


