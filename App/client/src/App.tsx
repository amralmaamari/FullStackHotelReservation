
import {Routes, Route } from "react-router-dom";
import Home from "./pages/home/Home";
import List from "./pages/list";
import Hotel from "./pages/hotel/Hotel";
import Register from "./pages/auth/Register";
import BookingConfirmation from "./pages/booking/Confirmation";
import BookingContinue from "./pages/booking/Continue";
import BookingSummaryPage from "./pages/booking/Summary";
import MyBookings from "./pages/booking/MyBookings";
import NotFound from "./pages/errors/NotFound";
import Navbar from "./components/layout/Navbar";

export default function App() {
  return (
    <>
     <div className="bg-primary pt-1 px-4">
        <Navbar />
      </div>
    <Routes>
      <Route path="/" element={<Home/>}/>
      <Route path="/hotels" element={<List/>}/>
      <Route path="/hotels/:id" element={<Hotel/>}/>
      <Route path="/hotel/booking-confirmation/:bookingId" element={<BookingConfirmation />} />
      <Route path="/booking/:bookingId/continue" element={<BookingContinue />} />
      <Route path="/booking/:bookingId/summary" element={<BookingSummaryPage />} />
      <Route path="/my-bookings" element={<MyBookings />} />
      <Route path="/auth" element={<Register />}/>
      <Route path="*" element={<NotFound />}/>
    </Routes>
    </>
  )
}
