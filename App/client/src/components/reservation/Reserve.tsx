import { useState } from "react";
import useFetch from "../../hooks/useFetch";
import { useLocation, useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { format } from "date-fns";
import { useSearchContext } from "../../context/searchContext";
import axios from "axios";
import toast from "react-hot-toast";
import { useAuth } from "../../context/AuthContext";

// Here By HotelID
interface IUnavailableDate {
  checkIn: string;
  checkOut: string;
  reason: string;
  isActive: boolean;
}

interface IRoomNumber {
  roomID: number;
  roomNumber: string;
  unavailableDates: IUnavailableDate[];
}

interface IRoomData {
  hotelID: number;
  price: number;
  roomTypeID: number;
  title: string;
  maxPeople: number;
  description: string;
  roomNumbers: IRoomNumber[];
}

interface IReserveProps {
  onClickClose: () => void;
}

export default function Reserve({ onClickClose }: IReserveProps) {
  const navigate = useNavigate();
  const location = useLocation();  
  const backendUrl = import.meta.env.VITE_API_URL; // Ensure your environment variable is set

  const { id } = useParams<{ id: string }>();
    const { resetSearch } = useSearchContext();

  const [selectedRooms, setSelectedRooms] = useState<string[]>([]);
  const {user} = useAuth();
  const { data, error, loading, } = useFetch({
    url: `/Hotel/rooms/details?id=${id}`,
  });
  console.log(data);
  
  const { selectionRange } = useSearchContext();

  if (data && Array.isArray(data)) {
    data.map((data, index) => {
      console.log(`Item at index ${index}:`, data);
    });
  }

  const handleRoomReserveChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const roomID = e.target.value;
    if (e.target.checked) {
      setSelectedRooms((prev) => [...prev, roomID]);
    } else {
      setSelectedRooms((prev) => prev.filter((item) => item !== roomID));
    }
  };


  const onHandleSubmitReserve = async () => {
    if (selectedRooms.length === 0) {
      toast.error("Please select at least one room.");

      return;
    }
    if (
      selectionRange.startDate.toISOString().split("T")[0] ===
      selectionRange.endDate.toISOString().split("T")[0]
    ) {
      toast.error("Check-in and Check-out dates cannot be the same. Please select valid dates.");
      return;
    }
  if(!user){
    toast.error("Please Login to complete booking");
    navigate("/auth", { state: { from: location.pathname } });

    return;
  }

     

      
    try {
  
       const payload = {
        userEmail: user.email,
        bookingRoomsId: selectedRooms,
        checkIn: format(selectionRange.startDate, "yyyy-MM-dd"),   
        checkOut: format(selectionRange.endDate, "yyyy-MM-dd"),
      };
      

      const response = await axios.post(`${backendUrl}/Booking`, payload);
      console.log(response);
      
      if(response.data.success){
        const bookingId = response.data.data.bookingID ;
        if (!bookingId) {
            throw new Error("No booking ID returned from server");
          }
        toast.success("Reserve successful!");
          // Navigate to confirmation page
          navigate(`/hotel/booking-confirmation/${bookingId}`);
          resetSearch();
      }
      
    } catch (error: any) {
      toast.error("Booking failed:", error);
    }
  };
  const handleChecked = (room) => {
    // Check if any unavailable date range overlaps with the selectionRange
    const isUnavailable = room.unavailableDates.some((date) => {
      const start = new Date(date.checkIn);
      const end = new Date(date.checkOut);
      const selectedStart = new Date(selectionRange.startDate);
      const selectedEnd = new Date(selectionRange.endDate);

      return start <= selectedEnd && selectedStart <= end;
    });

    return isUnavailable;
  };

 

  return (
    <div className="fixed inset-0 z-50 bg-black bg-opacity-50 flex items-center justify-center">
  <div className="relative w-[95%] md:w-4/5 lg:w-3/5 max-h-[85vh] overflow-auto bg-white rounded-xl shadow-lg p-6">
    {/* Close Button */}
    <button
      className="absolute top-3 right-4 text-3xl text-gray-700 hover:text-red-500 transition"
      onClick={onClickClose}
    >
      &times;
    </button>

    {loading && (
      <div className="text-center text-gray-600 text-lg py-4">Loading available rooms...</div>
    )}

    {!loading && error && (
      <div className="text-center text-red-600 bg-red-100 px-4 py-2 rounded-md mb-4">{error}</div>
    )}

    {!error && data && (
      <div>
        <h3 className="text-2xl font-bold text-gray-800 mb-4">🛏️ Select Your Rooms</h3>

        <div className="space-y-6">
          {data.map((room, index) => (
            <div
              key={room.hotelID}
              className="bg-gray-50 p-4 rounded-md shadow-sm border border-gray-200"
            >
              {/* Room Info */}
              <div className="mb-2">
                <h4 className="text-lg font-semibold text-blue-700">{room.title}</h4>
                <p className="text-sm text-gray-600 mt-1">{room.description}</p>
              </div>

              {/* Capacity & Price */}
              <div className="flex flex-wrap justify-between items-center mt-2 text-sm text-gray-700">
                <div>👥 Max People: <b>{room.maxPeople}</b></div>
                <div>💰 Price: <span className="font-bold text-green-600">${room.price}</span></div>
              </div>

              {/* Room Numbers */}
              <div className="mt-4 grid grid-cols-3 sm:grid-cols-4 md:grid-cols-6 gap-3">
                {room.roomNumbers.map((roomNumber) => {
                  const isDisabled = handleChecked(roomNumber);

                  return (
                    <label
                      key={roomNumber.roomID}
                      className={`flex items-center gap-2 px-2 py-1 border rounded-md text-sm cursor-pointer transition 
                        ${isDisabled ? 'bg-gray-200 text-gray-400 cursor-not-allowed' : 'hover:bg-blue-50 border-gray-300'}`}
                    >
                      <input
                        type="checkbox"
                        disabled={isDisabled}
                        className="w-4 h-4"
                        onChange={handleRoomReserveChange}
                        value={roomNumber.roomID}
                        name={roomNumber.roomNumber}
                        id={roomNumber.roomNumber}
                      />
                      {roomNumber.roomNumber}
                    </label>
                  );
                })}
              </div>
            </div>
          ))}
        </div>

        {/* Submit Button */}
        <button
          className="mt-6 w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition text-lg font-medium"
          onClick={onHandleSubmitReserve}
        >
          ✅ Reserve Now!
        </button>
      </div>
    )}
  </div>
</div>

  );
}
