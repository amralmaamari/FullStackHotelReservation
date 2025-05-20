// src/pages/MyBookings.tsx
import { useEffect, useState } from "react";
import { useLocation, useNavigate, Link } from "react-router-dom";
import toast from "react-hot-toast";
import axios from "axios";
import { format } from "date-fns";
import Navbar from "../../components/layout/Navbar";
import Loading from "../../components/response/Loading";
import Error from "../../components/response/Error";
import { useAuth } from "../../context/AuthContext";
import useFetch from "../../hooks/useFetch";

interface BookingData {
  bookingId: string;
  status: "Pending" | "Confirmed" | "Cancelled";
  roomTypes: string[];
  checkIn: string;
  checkOut: string;
  totalNights: number;
  totalPrice: number;
}

export default function MyBookings() {
  const navigate = useNavigate();
  
  const location = useLocation();
  const { user } = useAuth();
  const { data, error, loading,reFetch } = useFetch({
    url: `/booking/my-booking?email=${user?.email}`,
  });

  const bookings = (data ?? []) as BookingData[];
  console.log("bookings" + bookings);

  const [cancelingId, setCancelingId] = useState<string | null>(null);
  const apiUrl = import.meta.env.VITE_API_URL;

  const confirmCancel = async () => {
    if (!cancelingId) return;
    try {
      const response = await axios.patch(
        `${apiUrl}/booking/${cancelingId}/cancel`
      );
      console.log("fromCancle" + response);
      
      if (response.data.success) {
        toast.success("Your booking has been cancelled successfully.");
        reFetch();
      } else {
        toast.error("We couldn't cancel your booking. Please try again.");
      }
    } catch {
      toast.error("Failed to cancel booking");
    } finally {
      setCancelingId(null);
    }
  };

  if (!user) {
    navigate("/", { state: { from: location.pathname } });
  }

  return (
    <>
   

      <main className="min-h-screen bg-gray-100 py-12">
        <div className="max-w-5xl mx-auto px-4">
          <h1 className="text-4xl font-extrabold text-blue-700 mb-8">
            📋 My Bookings
          </h1>

          {loading && <Loading />}
          {!loading && error && <Error error={error} />}
          {!loading && !error && bookings.length === 0 && (
            <p className="text-center text-gray-500">
              You have no bookings yet.
            </p>
          )}

          <div className="space-y-8">
            {bookings.map((b) => (
              <BookingCard
                key={b.bookingId}
                booking={b}
                onContinue={() => navigate(`/booking/${b.bookingId}/continue`)}
                onSummary={() => navigate(`/booking/${b.bookingId}/summary`)}
                onCancel={() => setCancelingId(b.bookingId)}
              />
            ))}
          </div>

          {cancelingId && (
            <CancelModal
              bookingId={cancelingId}
              onConfirm={confirmCancel}
              onClose={() => setCancelingId(null)}
            />
          )}
        </div>
      </main>
    </>
  );
}

// Reusable Booking Card
function BookingCard({
  booking,
  onContinue,
  onSummary,
  onCancel,
}: {
  booking: BookingData;
  onContinue: () => void;
  onSummary: () => void;
  onCancel: () => void;
}) {
  return (
    <div className="bg-white rounded-2xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow">
      <header className="bg-gradient-to-r from-blue-600 to-indigo-600 px-6 py-4 flex justify-between items-center">
        <h2 className="text-xl font-semibold text-white">
          Booking #{booking.bookingId}
        </h2>
        <span
          className={`px-3 py-1 text-sm font-medium rounded-full ${
            booking.status === "Confirmed"
              ? "bg-green-100 text-green-800"
              : booking.status === "Pending"
              ? "bg-yellow-100 text-yellow-800"
              : "bg-red-100 text-red-800"
          }`}
        >
          {booking.status}
        </span>
      </header>

      <div className="px-6 py-6 grid grid-cols-1 sm:grid-cols-2 gap-6 text-gray-700">
        <Detail label="Rooms" value={booking.roomTypes.join(", ")} icon="🏨" />
        <Detail
          label="Check-in"
          value={format(new Date(booking.checkIn), "MMM d, yyyy")}
          icon="📅"
        />
        <Detail
          label="Check-out"
          value={format(new Date(booking.checkOut), "MMM d, yyyy")}
          icon="📆"
        />
        <Detail
          label="Nights"
          value={booking.totalNights.toString()}
          icon="🛏️"
        />
        <Detail
          label="Total"
          value={`$${booking.totalPrice.toFixed(2)}`}
          icon="💵"
        />
      </div>

      <div className="border-t border-gray-200" />

      <div className="px-6 py-4 flex flex-wrap justify-end gap-3 bg-gray-50">
        {booking.status === "Pending" && (
          <>
            <button
              onClick={onContinue}
              className="
    inline-flex items-center gap-2
    bg-gradient-to-r from-green-500 to-green-600
    hover:from-green-600 hover:to-green-700
    text-white font-semibold
    py-2 px-5
    rounded-full
    shadow-lg
    transform transition
    hover:scale-105
    focus:outline-none focus:ring-4 focus:ring-green-300
  "
            >
              <span className="text-lg">💳</span>
              <span>Continue Payment</span>
            </button>

            <button
              onClick={onCancel}
              className="
    inline-flex items-center gap-2
    bg-gradient-to-r from-red-500 to-red-600
    hover:from-red-600 hover:to-red-700
    text-white font-semibold
    py-2 px-5
    rounded-full
    shadow-lg
    transform transition
    hover:scale-105
    focus:outline-none focus:ring-4 focus:ring-red-300
  "
            >
              <span className="text-lg">❌</span>
              <span>Cancel Booking</span>
            </button>
          </>
        )}
        {(booking.status === "Confirmed" || booking.status === "Cancelled") && (
          <button onClick={onSummary} className="btn-secondary">
            📄 View Summary
          </button>
        )}
      </div>
    </div>
  );
}

// Reusable Detail row
function Detail({
  label,
  value,
  icon,
}: {
  label: string;
  value: string;
  icon: string;
}) {
  return (
    <div className="flex items-center gap-3">
      <span className="text-2xl">{icon}</span>
      <div>
        <p className="text-sm text-gray-500">{label}</p>
        <p className="font-medium">{value}</p>
      </div>
    </div>
  );
}

// Modal for cancellation confirmation
function CancelModal({
  bookingId,
  onConfirm,
  onClose,
}: {
  bookingId: string;
  onConfirm: () => void;
  onClose: () => void;
}) {
  return (
    <div className="fixed inset-0 bg-black bg-opacity-30 flex items-center justify-center z-50">
      <div className="bg-white rounded-xl p-6 max-w-sm text-center shadow-xl">
        <h3 className="text-lg font-semibold mb-4">❗ Confirm Cancellation</h3>
        <p className="text-gray-600 mb-6">
          Are you sure you want to cancel booking #{bookingId}?
        </p>
        <div className="flex justify-center gap-4">
          <button onClick={onConfirm} className="btn-danger px-4 py-2">
            Yes, Cancel
          </button>
          <button onClick={onClose} className="btn-secondary px-4 py-2">
            No, Go Back
          </button>
        </div>
      </div>
    </div>
  );
}
