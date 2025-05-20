using System.Security.Claims;
using Hotel_Business;
using HotelDataAccessLayer;
using HotelReservationDataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HotelReservationAPI.Controllers.BookingController;
using static HotelReservationAPI.Controllers.Model.BookingDTO;

namespace HotelReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookingController : Controller
    {
        public class AddNewBookingDTO
        {
            public string UserEmail { get; set; }
            public List<int> BookingRoomsId { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
        }
        private readonly clsJwtTokenService _jwtTokenService;
        public BookingController(clsJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }
        private decimal _CalcRoomFees(int TotalDays, decimal PricePerDay)
        {

            decimal totalPrice = TotalDays * PricePerDay;
            return totalPrice;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddNewBooking([FromBody] AddNewBookingDTO addNewBookingDTO)
        {
            if (addNewBookingDTO == null)
                return BadRequest(new { success = false, message = "Invalid booking data." });

            if (addNewBookingDTO.CheckIn >= addNewBookingDTO.CheckOut)
                return BadRequest(new { success = false, message = "Invalid booking date range." });

            //var emailClaim = User.FindFirst(ClaimTypes.Email) ?? User.FindFirst("email");
            //if (emailClaim == null)
            //    return BadRequest(new { success = false, message = "Invalid token: Email claim not found." });

            //string email = emailClaim.Value;

            UserDTO foundUser = clsUsers.GetUsersInfoByEmail(addNewBookingDTO.UserEmail);
            if (foundUser == null)
                return BadRequest(new { success = false, message = "User not found for booking." });

            //if (foundUser.Email != email)
            //    return Forbid("You can only book using your own account.");

            var bookingDTO = new BookingDTO(
                -1,
                foundUser.UserID,
                addNewBookingDTO.CheckIn,
                addNewBookingDTO.CheckOut,
                1, // Status: pending
                DateTime.Now,
                DateTime.Now
            );

            clsBooking booking = new clsBooking(bookingDTO, clsBooking.enMode.AddNew);
            booking.Save();

            if (booking.BookingID <= 0)
                return BadRequest(new { success = false, message = "Booking could not be created." });

            int totalNights = Math.Max(1, (addNewBookingDTO.CheckOut - addNewBookingDTO.CheckIn).Days);
            decimal totalPrice = 0;
            List<string> roomNames = new List<string>();

            foreach (var roomId in addNewBookingDTO.BookingRoomsId)
            {
                clsRooms foundRoom = clsRooms.GetRoomsInfoByID(roomId);
                if (foundRoom == null)
                    return BadRequest(new { success = false, message = $"Room with ID {roomId} not found." });

                decimal price = _CalcRoomFees(totalNights, foundRoom.Price);

                var bookingRoomDTO = new BookingRoomDTO(-1, booking.BookingID, foundRoom.RoomID, price);
                clsBookingRoom bookingRoom = new clsBookingRoom(bookingRoomDTO, clsBookingRoom.enMode.AddNew);
                bookingRoom.Save();

                if (bookingRoom.BookingRoomID <= 0)
                    return BadRequest(new { success = false, message = $"BookingRoom for RoomID {roomId} could not be created." });

                //here i have add  RoomUnivalabeDates
                var roomUnavailableDatesDTO = new RoomUnavailableDatesDTO(-1, roomId, addNewBookingDTO.CheckIn, addNewBookingDTO.CheckOut, "Rent Room", true);
                clsRoomUnavailableDates roomUnavailableDates = new clsRoomUnavailableDates(roomUnavailableDatesDTO, clsRoomUnavailableDates.enMode.AddNew);
                roomUnavailableDates.Save();
                if (roomUnavailableDates.RoomUnavailableDateID <= 0)
                    return BadRequest(new { success = false, message = $"RoomUnavailableDates for RoomID {roomId} could not be created." });


                roomNames.Add(foundRoom.RoomTypeInfo.Title);
                totalPrice += price;
            }

            return Ok(new
            {
                success = true,
                data = new
                {
                    BookingID = booking.BookingID,
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                    Nights = totalNights,
                    RoomTypes = roomNames,
                    TotalPrice = totalPrice,
                    Status = booking.BookingStatusInfo.StatusName
                }
            });
        }




        [HttpGet("{id}/confirmation", Name = "getBookingConfirmationById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetBookingConfirmationById(int id)
        {
            if (id < 1)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid booking ID provided."
                });
            }

            var bookingDetails = clsBooking.GetBookingDetailsByID(id);

            if (bookingDetails == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No booking found with ID {id}."
                });
            }

            var bookingStatus = clsBookingStatus.GetBookingStatusInfoByID(bookingDetails.BookingStatusID);
            if (bookingStatus == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Invalid booking status ID: {bookingDetails.BookingStatusID}."
                });
            }

            var totalNights = Math.Max(1, (bookingDetails.CheckOut - bookingDetails.CheckIn).Days);
            var totalPrice = 0m;
            var roomTypes = new HashSet<string>(); // للتخلص من التكرار

            if (bookingDetails.Rooms is not null)
            {
                foreach (var room in bookingDetails.Rooms)
                {
                    roomTypes.Add(room.RoomType);
                    totalPrice += room.PaidFees;
                }
            }

            return Ok(new
            {
                success = true,
                data = new
                {
                    bookingId = bookingDetails.BookingID,
                    status = bookingStatus.StatusName,
                    roomTypes = roomTypes, // الآن يظهر كل أنواع الغرف
                    checkIn = bookingDetails.CheckIn,
                    checkOut = bookingDetails.CheckOut,
                    totalNights = totalNights,
                    totalPrice = totalPrice
                }
            });
        }


        [HttpGet("my-booking", Name = "getBookingByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetUserBooking([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid booking email provided."
                });
            }
            var user = clsUsers.GetUsersInfoByEmail(email);
            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No bookings found for user Email {email}."
                });
            }

            var bookings = clsBooking.GetBookingDetailsByUserID(user.UserID);
            if (bookings == null || !bookings.Any())
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No bookings found for user ID {user.UserID}."
                });
            }

            var summaries = bookings.Select(booking =>
            {
                // أ: الحالة
                var statusInfo = clsBookingStatus.GetBookingStatusInfoByID(booking.BookingStatusID);
                var statusName = statusInfo?.StatusName ?? "Unknown";

                // ب: عدد الليالي
                var totalNights = Math.Max(1, (booking.CheckOut - booking.CheckIn).Days);

                // ج: السعر الإجمالي وأنواع الغرف (بدون تكرار)
                var roomTypes = new HashSet<string>();
                decimal totalPrice = 0m;
                if (booking.Rooms != null)
                {
                    foreach (var room in booking.Rooms)
                    {
                        roomTypes.Add(room.RoomType);
                        totalPrice += room.PaidFees;
                    }
                }

                // د: ارجع anonymous object للملخص
                return new
                {
                    bookingId = booking.BookingID,
                    status = statusName,
                    roomTypes = roomTypes.ToList(),
                    checkIn = booking.CheckIn,
                    checkOut = booking.CheckOut,
                    totalNights = totalNights,
                    totalPrice = totalPrice
                };
            }).ToList();


            return Ok(new
            {
                success = true,
                data = summaries
            });
        }



        [HttpGet("{id}/booking-fees", Name = "getBookingFeesByBookingID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetTotalFeesForBookingID(int id)
        {
            if (id < 1)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid booking ID provided."
                });
            }

            var bookingDetails = clsBooking.GetBookingDetailsByID(id);

            if (bookingDetails == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No booking found with ID {id}."
                });
            }


            var totalPrice = 0m;

            if (bookingDetails.Rooms is not null)
            {
                foreach (var room in bookingDetails.Rooms)
                {
                    totalPrice += room.PaidFees;
                }
            }

            return Ok(new
            {
                success = true,
                data = new
                {
                    bookingId = bookingDetails.BookingID,
                    totalPrice = totalPrice
                }
            });
        }



        //not compeleted here 
        [HttpGet("{id}/summary", Name = "getBookingSummaryByBookingId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetBookingSummaryByBookingId(int id)
        {
            if (id < 1)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid booking ID provided."
                });
            }

            var bookingDetails = clsBooking.GetBookingDetailsByID(id);

            if (bookingDetails == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No booking found with ID {id}."
                });
            }

            var bookingStatus = clsBookingStatus.GetBookingStatusInfoByID(bookingDetails.BookingStatusID);
            if (bookingStatus == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Invalid booking status ID: {bookingDetails.BookingStatusID}."
                });
            }

            var totalNights = Math.Max(1, (bookingDetails.CheckOut - bookingDetails.CheckIn).Days);
            var totalPrice = 0m;
            var roomTypes = new HashSet<string>(); // للتخلص من التكرار

            if (bookingDetails.Rooms is not null)
            {
                foreach (var room in bookingDetails.Rooms)
                {
                    roomTypes.Add(room.RoomType);
                    totalPrice += room.PaidFees;
                }
            }



            List<GuestsDTO> guests = clsGuests.GetAllGuestsByBookingID(bookingDetails.BookingID);
            if (guests == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No bookingGuests found with BookingID {bookingDetails.BookingID}."
                });
            }

            List<string> guestsNames = new List<string>();
            foreach (var guest in guests)
            {
                guestsNames.Add(guest.FullName);
            }


            clsPayments payments = clsPayments.GetPaymentsInfoByBookingID(bookingDetails.BookingID);
            object payment = null;
            if (payments != null)
            {
                payment = new
                {
                    PaymentTypeName = payments.PaymentTypesInfo?.PaymentTypeName,
                    Amount = payments.Amount,
                    CurrencyCode = payments.CurrencyCode
                };
            }


            return Ok(new
            {
                success = true,
                data = new
                {
                    bookingId = bookingDetails.BookingID,
                    status = bookingStatus.StatusName,
                    roomTypes = roomTypes, // الآن يظهر كل أنواع الغرف
                    checkIn = bookingDetails.CheckIn,
                    checkOut = bookingDetails.CheckOut,
                    totalNights = totalNights,
                    totalPrice = totalPrice,
                    guestsNames,
                    payment
                }
            });
        }



        [HttpPost("{id}/complete", Name = "AddGustsAndPaymentForBooking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult AddGustsAndPaymentForBooking([FromBody] AddGustsAndPaymentDTO addGustsAndPaymentDTO)
        {

            if (addGustsAndPaymentDTO == null)
            {
                return BadRequest(new { success = false, message = "Invalid GustsAndPayment data." });
            }

            if (addGustsAndPaymentDTO.BookingId < 1)
            {
                return BadRequest(new { success = false, message = "Invalid BookingID." });
            }


            if (addGustsAndPaymentDTO.Guests != null)
            {
                foreach (var guest in addGustsAndPaymentDTO.Guests)
                {
                    GuestsDTO guestsDTO = new GuestsDTO(-1, guest.IdNumber, guest.FullName);

                    clsGuests newGuest = new clsGuests(guestsDTO, clsGuests.enMode.AddNew);
                    newGuest.Save();
                    if (newGuest == null)
                    {
                        return NotFound(new
                        {
                            success = false,
                            message = $"Failed To Add Guest  ."
                        });
                    }

                    BookingGuestsDTO newbookingGuestsDTO = new BookingGuestsDTO(-1, addGustsAndPaymentDTO.BookingId, newGuest.GuestID);
                    clsBookingGuests bookingGuests = new clsBookingGuests(newbookingGuestsDTO, clsBookingGuests.enMode.AddNew);
                    bookingGuests.Save();
                    if (newGuest == null)
                    {
                        return NotFound(new
                        {
                            success = false,
                            message = $"Failed To Add BookingGuests ."
                        });
                    }
                }

            }

            if (addGustsAndPaymentDTO.Payment == null)
            {
                return BadRequest(new { success = false, message = "Payment is required." });

            }
            PaymentsDTO paymentDTO = new PaymentsDTO(-1, addGustsAndPaymentDTO.BookingId, addGustsAndPaymentDTO.Payment.Type
                , 1, addGustsAndPaymentDTO.Payment.Amount, addGustsAndPaymentDTO.Payment.Currency, DateTime.Now);

            clsPayments payments = new clsPayments(paymentDTO, clsPayments.enMode.AddNew);
            payments.Save();

            if (payments.PaymentID < 1)
            {
                return BadRequest(new { success = false, message = "Paymentnot added." });
            }

            clsBooking booking = clsBooking.GetBookingInfoByID(addGustsAndPaymentDTO.BookingId);
            if (booking == null)
            {
                return BadRequest(new { success = false, message = "Booking not found BookingID." });

            }

            booking.BookingStatusID = 2;// equal to Confirmed

            if (booking.Save())
            {

                return Ok(new
                {
                    success = true,
                });
            }

            return BadRequest(new { success = false, message = "Not Added to Booking or Guest or Payment" });


        }


        [HttpPatch("{id}/cancel", Name = "CancleBooking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult CancleBooking(int id)
        {
            if (id < 1)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid booking ID provided."
                });
            }

            clsBooking booking = clsBooking.GetBookingInfoByID(id);

            if (booking == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No booking found with ID {id}."
                });
            }

            bool isDeactivateRoomUnavailableByBooking = clsRoomUnavailableDates.DeactivateRoomUnavailableByBooking(booking.BookingID);

            if (!isDeactivateRoomUnavailableByBooking)
                return NotFound(new
                {
                    success = false,
                    message = $"No DeactivateRoomUnavailable found with BookingID {id}."
                });

            booking.BookingStatusID = 5;
            if (!booking.Save())
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Not Booking Update  BookingID {id}."
                });
            }
            return Ok(new
            {
                success = true
            });
        }

        //POST    /api/Reservation           // لإضافة حجز جديد
        //GET     /api/Reservation           // لجلب كل الحجوزات
        //GET     /api/Reservation/{id//}      // لجلب حجز معين
        //PUT / api / Reservation /{ id}      // لتعديل حجز
        //DELETE / api / Reservation /{ id}      // لحذف حجز
    }
}
