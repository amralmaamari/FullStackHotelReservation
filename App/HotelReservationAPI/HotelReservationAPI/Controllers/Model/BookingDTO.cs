using HotelDataAccessLayer;

namespace HotelReservationAPI.Controllers.Model
{
    public class BookingDTO
    {
        public class GuestDTO
        {
            /// <summary>
            /// Full name of the guest.
            /// </summary>
            public string FullName { get; set; }

            /// <summary>
            /// Identification number of the guest.
            /// </summary>
            public string IdNumber { get; set; }
        }

        /// <summary>
        /// Represents payment information for a booking.
        /// </summary>
        public class PaymentDTO
        {
            /// <summary>
            /// Payment method type (e.g., "Card", "Cash").
            /// </summary>
            public int Type { get; set; }

            /// <summary>
            /// Amount to be paid.
            /// </summary>
            public decimal Amount { get; set; }

            /// <summary>
            /// Currency code (e.g., "USD").
            /// </summary>
            public string Currency { get; set; }
        }

        public class AddGustsAndPaymentDTO
        {
            /// <summary>
            /// The identifier of the booking.
            /// </summary>
            public int BookingId { get; set; }

            /// <summary>
            /// List of guests associated with this booking.
            /// May be null if no guests are being added.
            /// </summary>
            public List<GuestDTO>? Guests { get; set; }

            /// <summary>
            /// Payment details for the booking.
            /// </summary>
            public PaymentDTO Payment { get; set; }
        }
    }
}
