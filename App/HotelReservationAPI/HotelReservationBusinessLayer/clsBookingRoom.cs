
using System;
using System.Data;
using System.Data.SqlClient;
using HotelDataAccessLayer;

namespace Hotel_Business
{
    public class clsBookingRoom
    {


        public enum enMode { AddNew = 0, Update = 1 }
        public static enMode Mode = enMode.AddNew;

        public BookingRoomDTO bookingroomDTO
        {
            get
            {
                return new BookingRoomDTO(
              this.BookingRoomID,
              this.BookingID,
              this.RoomID,
              this.PaidFees
        
               );
            }
        }

        public int BookingRoomID { get; set; }
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public clsRooms RoomInfo { get; set; }
        public decimal PaidFees { get; set; }


        public clsBookingRoom()
        {
            this.BookingRoomID = -1;

            this.BookingID = -1;

            this.RoomID = -1;

            this.PaidFees = 0m;

            Mode = enMode.AddNew;
        }

        public clsBookingRoom(BookingRoomDTO bookingroom, enMode mode = enMode.AddNew)
        {

            this.BookingRoomID = bookingroom.BookingRoomID;

            this.BookingID = bookingroom.BookingID;

            this.RoomID = bookingroom.RoomID;

            RoomInfo=clsRooms.GetRoomsInfoByID(this.RoomID);

            this.PaidFees = bookingroom.PaidFees;

            Mode = mode;
        }
        public static List<BookingRoomDTO> GetAllBookingRoom()
        {
            return clsBookingRoomData.GetAllBookingRoom();

        }


        public static clsBookingRoom GetBookingRoomInfoByID(int BookingRoomID)
        {
            BookingRoomDTO bookingroomDTO = clsBookingRoomData.GetBookingRoomInfoByID(BookingRoomID);

            if (bookingroomDTO != null)
            {
                return new clsBookingRoom(bookingroomDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewBookingRoom()
        {

            this.BookingRoomID = (int)clsBookingRoomData.AddNewBookingRoom(this.bookingroomDTO);
            return (this.BookingRoomID != -1);

        }

        private bool _UpdateBookingRoom()
        {

            return (clsBookingRoomData.UpdateBookingRoom(this.bookingroomDTO));
        }

        public bool Save()
        {

            if (Mode == enMode.AddNew)
            {
                if (_AddNewBookingRoom())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return _UpdateBookingRoom();
            }

        }

        public static bool DeleteBookingRoom(int BookingRoomID)
        {
            return clsBookingRoomData.DeleteBookingRoom(BookingRoomID);

        }



    }
}


