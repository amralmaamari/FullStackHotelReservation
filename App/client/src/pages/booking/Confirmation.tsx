import { useParams, useNavigate } from 'react-router-dom';
import Loading from '../../components/response/Loading';
import Error from '../../components/response/Error';
import useFetch from '../../hooks/useFetch';

type BookingData = {
  bookingId: number;
  status: string;
  roomTypes: string[];
  checkIn: string;
  checkOut: string;
  totalNights: number;
  totalPrice: number;
};

export default function BookingConfirmation() {
  const { bookingId } = useParams<{ bookingId: string }>();
  const navigate = useNavigate();

  const { data  , loading, error } = useFetch({
    url: `/Booking/${bookingId}/confirmation`
  });


  const booking = data as BookingData | null;

  return (
    <>
     

      <div className="min-h-screen bg-gradient-to-br from-blue-50 to-white flex flex-col items-center justify-center px-4">
        {loading && <Loading />}
        {!loading && error && <Error error={error} />}

        {!loading && !error && booking && (
          <div className="w-full max-w-7xl bg-white shadow-2xl rounded-2xl p-8 relative border border-blue-100">
            <div className="flex flex-col items-center text-center">
              {/* <div className="text-5xl mb-4 text-green-500">✅</div> */}
              <h1 className="text-3xl font-bold text-gray-800">Booking Confirmed</h1>
              <p className="text-gray-500 mt-2">Thank you for your reservation.</p>
            </div>

            <div className="mt-8 grid grid-cols-1 sm:grid-cols-2 gap-4">
              <InfoCard label="Booking ID" value={booking.bookingId.toString()} icon="🆔" />
              <InfoCard label="Status" value={booking.status} icon="📌" />

              {Array.isArray(booking.roomTypes) &&
                booking.roomTypes.map((room, index) => (
                  <InfoCard
                    key={index}
                    label={`Room ${index + 1}`}
                    value={room}
                    icon="🏨"
                  />
                ))}

              <InfoCard label="Check-in" value={booking.checkIn.slice(0, 10)} icon="📅" />
              <InfoCard label="Check-out" value={booking.checkOut.slice(0, 10)} icon="📆" />
              <InfoCard label="Nights" value={booking.totalNights.toString()} icon="🛏️" />
              <InfoCard label="Total" value={`$${booking.totalPrice}`} icon="💵" />
            </div>

            <div className="mt-6 flex flex-col sm:flex-row gap-4 justify-center">
              <button
                onClick={() => navigate(`/booking/${booking.bookingId}/continue`)}
                className="flex items-center gap-2 px-6 py-2 bg-green-600 text-white rounded-full shadow-lg hover:brightness-110 active:scale-95 transition-all duration-200"
              >
                <span>📝</span>
                <span className="font-medium">Continue Reservation</span>
              </button>

              <button
                onClick={() => navigate('/')}
                className="flex items-center gap-2 px-6 py-2 bg-gradient-to-r from-blue-500 to-indigo-600 text-white rounded-full shadow-lg hover:brightness-110 active:scale-95 transition-all duration-200"
              >
                <span>🏠</span>
                <span className="font-medium">Back to Home</span>
              </button>
            </div>
          </div>
        )}
      </div>
    </>
  );
}

function InfoCard({
  label,
  value,
  icon,
}: {
  label: string;
  value: string;
  icon: string;
}) {
  return (
    <div className="flex items-center gap-4 p-4 bg-blue-50 rounded-lg border border-blue-200 shadow-sm">
      <div className="text-2xl">{icon}</div>
      <div>
        <div className="text-sm text-gray-500">{label}</div>
        <div className="text-lg font-medium text-gray-800">{value}</div>
      </div>
    </div>
  );
}
