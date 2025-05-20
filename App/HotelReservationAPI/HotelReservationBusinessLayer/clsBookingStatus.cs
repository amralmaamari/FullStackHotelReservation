
using System;
using System.Data;
using System.Data.SqlClient;
using HotelDataAccessLayer;

namespace Hotel_Business
{
    public class clsBookingStatus
    {


        public enum enMode { AddNew = 0, Update = 1 }
        public static enMode Mode = enMode.AddNew;

        public BookingStatusDTO bookingstatusDTO
        {
            get
            {
                return new BookingStatusDTO(
              this.BookingStatusID,
              this.StatusName
        
               );
            }
        }

        public int BookingStatusID { get; set; }
        public string StatusName { get; set; }


        public clsBookingStatus()
        {
            this.BookingStatusID = -1;

            this.StatusName = "";

            Mode = enMode.AddNew;
        }

        public clsBookingStatus(BookingStatusDTO bookingstatus, enMode mode = enMode.AddNew)
        {

            this.BookingStatusID = bookingstatus.BookingStatusID;

            this.StatusName = bookingstatus.StatusName;

            Mode = mode;
        }
        public static List<BookingStatusDTO> GetAllBookingStatus()
        {
            return clsBookingStatusData.GetAllBookingStatus();

        }


        public static clsBookingStatus GetBookingStatusInfoByID(int BookingStatusID)
        {
            BookingStatusDTO bookingstatusDTO = clsBookingStatusData.GetBookingStatusInfoByID(BookingStatusID);

            if (bookingstatusDTO != null)
            {
                return new clsBookingStatus(bookingstatusDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewBookingStatus()
        {

            this.BookingStatusID = (int)clsBookingStatusData.AddNewBookingStatus(this.bookingstatusDTO);
            return (this.BookingStatusID != -1);

        }

        private bool _UpdateBookingStatus()
        {

            return (clsBookingStatusData.UpdateBookingStatus(this.bookingstatusDTO));
        }

        public bool Save()
        {

            if (Mode == enMode.AddNew)
            {
                if (_AddNewBookingStatus())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return _UpdateBookingStatus();
            }

        }

        public static bool DeleteBookingStatus(int BookingStatusID)
        {
            return clsBookingStatusData.DeleteBookingStatus(BookingStatusID);

        }



    }
}


