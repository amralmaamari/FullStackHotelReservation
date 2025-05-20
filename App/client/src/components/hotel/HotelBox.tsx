import { Link } from "react-router-dom";
import { IHotel, IPhoto } from "../../interface/Interface";
import { useSearchContext } from "../../context/searchContext";
import toast from "react-hot-toast";


interface IHotelBoxProps {
  hotel: IHotel;
}
export default function HotelBox({ hotel }: IHotelBoxProps) {
  const {selectionRange} = useSearchContext();

  const handleAvailabilityButton =()=>{
      const { startDate, endDate } = selectionRange;
    
      if (!startDate || !endDate) {
        toast.error("Please select both check-in and check-out dates.");
        return false;
      }
    
      if (startDate.getTime() === endDate.getTime()) {
        toast.error("Check-out date must be after check-in date.");
        return false;
      }
    
      if (endDate.getTime() < startDate.getTime()) {
        toast.error("Invalid date range. Please check your selection.");
        return false;
      }
    
      return true; // ✅ Passed all checks
    };
    
  
  const SVG_START =
    '<svg xmlns="http://www.w3.org/2000/svg" fill="#ffb700"   viewBox="0 0 24 24" width="10px"><path d="M23.555 8.729a1.505 1.505 0 0 0-1.406-.98h-6.087a.5.5 0 0 1-.472-.334l-2.185-6.193a1.5 1.5 0 0 0-2.81 0l-.005.016-2.18 6.177a.5.5 0 0 1-.471.334H1.85A1.5 1.5 0 0 0 .887 10.4l5.184 4.3a.5.5 0 0 1 .155.543l-2.178 6.531a1.5 1.5 0 0 0 2.31 1.684l5.346-3.92a.5.5 0 0 1 .591 0l5.344 3.919a1.5 1.5 0 0 0 2.312-1.683l-2.178-6.535a.5.5 0 0 1 .155-.543l5.194-4.306a1.5 1.5 0 0 0 .433-1.661"></path></svg>';
  const SVG_CHECK =
    '<svg xmlns="http://www.w3.org/2000/svg"  fill="#008234" viewBox="0 0 128 128" width="15px"><path d="M56.33 102a6 6 0 0 1-4.24-1.75L19.27 67.54A6.014 6.014 0 1 1 27.74 59l27.94 27.88 44-58.49a6 6 0 1 1 9.58 7.22l-48.17 64a6 6 0 0 1-4.34 2.39z"></path></svg>';

  return (
    <div className="flex flex-col md:flex-row bg-slate-200 rounded-md overflow-hidden shadow-sm">
    {/* Image */}
    <div className="w-full md:w-1/4">
      {hotel.photos.length > 0 ? (
        <img
          className="w-full h-52 md:h-full object-cover"
          src={hotel.photos[0].photoURL}
          alt="Hotel"
        />
      ) : (
        <div className="h-52 flex items-center justify-center text-gray-500">
          No image
        </div>
      )}
    </div>
  
    {/* Middle Content */}
    <div className="flex-1 p-2 sm:p-4">
      <h2 className="text-lg sm:text-xl font-bold">{hotel.name}</h2>
      <div className="flex mt-1 gap-1" dangerouslySetInnerHTML={{ __html: SVG_START.repeat(3) }} />
  
      <div className="text-sm text-blue-600 mt-2">
        {hotel.city} City, {hotel.address} <span className="text-gray-500"> • {hotel.distance}</span>
      </div>
  
      <div className="mt-3">
        <p className="font-medium text-sm">{hotel.title}</p>
        <p className="text-sm text-gray-700">{hotel.description}</p>
      </div>
  
      <ul className="mt-2 space-y-1 text-sm text-green-700">
        <li className="flex items-center gap-1" dangerouslySetInnerHTML={{ __html: SVG_CHECK + ' Free cancellation' }} />
        <li className="flex items-center gap-1" dangerouslySetInnerHTML={{ __html: SVG_CHECK + ' No prepayment needed — pay at property' }} />
      </ul>
    </div>
  
    {/* Price and Rating */}
    <div className="w-full md:w-1/5 flex flex-col justify-between p-2 sm:p-4">
      <div>
        <div className="flex justify-between items-center">
          <div>
            <p className="text-sm font-semibold">Very Good</p>
            <p className="text-xs text-gray-500">43 reviews</p>
          </div>
          <span className="text-lg font-bold text-blue-600">8.9</span>
        </div>
      </div>
      <div className="mt-4">
        <p className="text-red-500 line-through text-sm">₹ 43,371</p>
        <p className="text-black font-bold text-lg">₹ 23,420</p>
      </div>
      <Link
        to={`./${hotel.hotelID}`}
        onClick={(e) => {
          const valid = handleAvailabilityButton();
          if (!valid) e.preventDefault(); // ⛔ منع الانتقال
        }}
        className="mt-3 text-center bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
      >
        See availability
      </Link>
    </div>
  </div>
  
  );
}
