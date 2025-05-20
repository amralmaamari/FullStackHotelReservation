import { useParams, useNavigate } from "react-router-dom";
import Navbar from "../../components/layout/Navbar";
import useFetch from "../../hooks/useFetch";

// Types based on the new API response
interface PaymentInfo {
  paymentTypeName: string;
  amount: number;
  currencyCode: string;
}
interface BookingSummary {
  bookingId: number;
  status: string;
  roomTypes: string[];
  checkIn: string;
  checkOut: string;
  totalNights: number;
  totalPrice: number;
  guestsNames: string[]; // now array of strings
  payment: PaymentInfo | null; // payement (not payment), can be null!
}

export default function BookingSummaryPage() {
  const { bookingId } = useParams<{ bookingId: string }>();
  const navigate = useNavigate();


   const { data, error, loading } = useFetch({ url: `/Booking/${bookingId}/summary` });
const bookingSummary = data as BookingSummary | null ;

  return (
    <>
 

      <main className="bg-gradient-to-br from-blue-50 to-white min-h-screen py-16">
        <div className="max-w-4xl mx-auto">
          <section className="bg-white/95 rounded-3xl shadow-2xl p-8 md:p-14 border border-blue-100">
            <header className="mb-8 text-center">
              <h1 className="text-5xl font-extrabold tracking-tight text-blue-700 mb-2 flex items-center justify-center gap-3">
                <span className="text-4xl">📋</span>
                Booking Summary
              </h1>
              <p className="text-gray-500 text-lg">
                All your reservation details in one place.
              </p>
            </header>

            {loading ? (
              <div className="flex justify-center items-center min-h-[200px]">
                <span className="animate-spin text-blue-500 text-5xl">⏳</span>
              </div>
            ) : error ? (
              <p className="text-center text-red-600 font-medium">{error}</p>
            ) : bookingSummary ? (
              <div className="space-y-10">
                {/* Status Pill */}
                <div className="flex justify-center mb-2">
                  {bookingSummary.status.toLowerCase() === "confirmed" ? (
                    <span className="inline-flex items-center gap-2 px-4 py-1 rounded-full bg-green-100 text-green-700 font-semibold shadow text-lg">
                      ✅ Confirmed
                    </span>
                  ) : (
                    <span className="inline-flex items-center gap-2 px-4 py-1 rounded-full bg-yellow-100 text-yellow-700 font-semibold shadow text-lg">
                      ⏸️ {bookingSummary.status}
                    </span>
                  )}
                </div>

                {/* Info Cards */}
                <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
                  <InfoCard label="Booking ID" value={String(bookingSummary.bookingId)} icon="🆔" />
                  <InfoCard label="Nights" value={String(bookingSummary.totalNights)} icon="🌙" />
                  <InfoCard label="Check-In" value={formatDate(bookingSummary.checkIn)} icon="🟢" />
                  <InfoCard label="Check-Out" value={formatDate(bookingSummary.checkOut)} icon="🔴" />
                  <InfoCard label="Total Amount" value={`$${bookingSummary.totalPrice}`} icon="💰" />
                </div>

                {/* Divider */}
                <div className="border-t border-dashed my-4" />

                {/* Room Types */}
                <Section title="Room Types" icon="🏨">
                  <div className="flex flex-wrap gap-3">
                    {bookingSummary.roomTypes.length > 0 ? bookingSummary.roomTypes.map((room, i) => (
                      <div
                        key={i}
                        className="px-5 py-2 bg-blue-100 text-blue-800 rounded-full font-semibold text-lg shadow-sm hover:scale-105 transition"
                      >
                        {room}
                      </div>
                    )) : (
                      <div className="text-gray-500">No room types</div>
                    )}
                  </div>
                </Section>

                {/* Guests */}
                <Section title="Guests" icon="🧑‍🤝‍🧑">
                  <div className="flex flex-wrap gap-3">
                    {bookingSummary.guestsNames && bookingSummary.guestsNames.length > 0 ? (
                      bookingSummary.guestsNames.map((name, i) => (
                        <div
                          key={i}
                          className="px-5 py-2 bg-green-100 text-green-900 rounded-full font-semibold text-lg shadow-sm"
                        >
                          {name}
                        </div>
                      ))
                    ) : (
                      <div className="text-gray-500">No guests</div>
                    )}
                  </div>
                </Section>

                {/* Payment */}
                <Section title="Payment Details" icon="💳">
                  {bookingSummary.payment ? (
                    <div className="grid grid-cols-2 sm:grid-cols-3 gap-4">
                      <PayInfo label="Type" value={bookingSummary.payment.paymentTypeName} />
                      <PayInfo label="Amount" value={`${bookingSummary.payment.amount} ${bookingSummary.payment.currencyCode}`} />
                    </div>
                  ) : (
                    <div className="text-gray-500">No payment information</div>
                  )}
                </Section>

                {/* Action Buttons */}
                <div className="flex flex-col md:flex-row gap-4 justify-center mt-10">
                  <ActionBtn
                    icon="🏠"
                    label="Home"
                    color="blue"
                    onClick={() => navigate("/")}
                  />
                  <ActionBtn
                    icon="📄"
                    label="My Bookings"
                    color="gray"
                    onClick={() => navigate("/my-bookings")}
                  />
                  <ActionBtn
                    icon="🖨️"
                    label="Print"
                    color="green"
                    onClick={() => window.print()}
                  />
                </div>
              </div>
            ) : null}
          </section>
        </div>
      </main>
    </>
  );
}

// Info Card
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
    <div className="flex items-center gap-4 bg-blue-50 rounded-2xl border border-blue-100 px-5 py-5 shadow hover:shadow-lg transition min-h-[80px]">
      <div className="text-2xl">{icon}</div>
      <div>
        <div className="text-xs text-gray-500">{label}</div>
        <div className="text-xl font-bold text-gray-900">{value}</div>
      </div>
    </div>
  );
}

// Section Wrapper
function Section({
  title,
  icon,
  children,
}: {
  title: string;
  icon: string;
  children: React.ReactNode;
}) {
  return (
    <div>
      <div className="flex items-center gap-2 mb-2 text-xl font-semibold text-blue-800">
        <span className="text-2xl">{icon}</span>
        {title}
      </div>
      <div>{children}</div>
    </div>
  );
}

// Payment Info
function PayInfo({ label, value }: { label: string; value: string }) {
  return (
    <div className="bg-yellow-50 rounded-lg px-4 py-2 text-center shadow border border-yellow-100">
      <div className="text-xs text-gray-500">{label}</div>
      <div className="text-base font-bold text-yellow-700">{value}</div>
    </div>
  );
}

// Call-To-Action Button
function ActionBtn({
  icon,
  label,
  color,
  onClick,
}: {
  icon: string;
  label: string;
  color: "blue" | "gray" | "green";
  onClick: () => void;
}) {
  const colorMap: Record<string, string> = {
    blue: "bg-blue-600 hover:bg-blue-700 text-white",
    gray: "bg-gray-600 hover:bg-gray-700 text-white",
    green: "bg-green-600 hover:bg-green-700 text-white",
  };
  return (
    <button
      onClick={onClick}
      className={`flex items-center justify-center gap-2 px-7 py-3 rounded-full font-semibold text-lg shadow ${colorMap[color]} transition-all hover:scale-105`}
    >
      <span className="text-xl">{icon}</span>
      {label}
    </button>
  );
}

// Helper to format ISO date string as yyyy-mm-dd or your preferred format
function formatDate(dateString: string) {
  if (!dateString) return '';
  const d = new Date(dateString);
  return d.toLocaleDateString();
}
