import { useEffect, useRef, useState } from "react";
import DatePicker from "../reservation/DatePicker";
import { useSearchContext } from "../../context/searchContext";

export default function SideSearchBox({ onClickSearch }) {
    const dateWrapperRef = useRef(null);

  const [openDate, setOpenDate] = useState(false);
  
  const {
    destination,
    setDestination,
    selectionRange,
    setSelectionRange,
    occupancy,
    setOccupancy,
    minPrice,
    setMinPrice,
    maxPrice,
    setMaxPrice,
  } = useSearchContext();
  useEffect(() => {
    function handleClickOutside(event) {
      if (
        openDate &&
        dateWrapperRef.current &&
        !dateWrapperRef.current.contains(event.target)
      ) {
        setOpenDate(false);
      }
    }
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [openDate]);
  return (
    <div className="w-full lg:w-1/3 px-2">
      <div className="sticky top-4 bg-white rounded-xl shadow-md p-4 border border-gray-200">
        <h2 className="text-xl font-semibold text-gray-800 mb-4">Search Hotels</h2>
        <form className="space-y-4">
          {/* Destination Input */}
          <div className="space-y-1">
            <label className="text-sm text-gray-600">Destination</label>
            <input
              type="text"
              value={destination}
              onChange={(e) => setDestination(e.target.value)}
              placeholder="Enter your destination"
              className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring focus:ring-blue-200 text-sm"
            />
          </div>

          {/* Date Picker */}
          <div className="space-y-1" ref={dateWrapperRef}>
            <label className="text-sm text-gray-600">Dates</label>
            <div
              className="w-full px-3 py-2 border border-gray-300 rounded-md bg-white text-sm cursor-pointer flex items-center justify-between"
              onClick={() => setOpenDate(!openDate)}
            >
              <span>{`${selectionRange.startDate.toLocaleDateString()} — ${selectionRange.endDate.toLocaleDateString()}`}</span>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="w-4 h-4 text-gray-500"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" />
              </svg>
            </div>
            {openDate && (
              <DatePicker
                selectionRange={selectionRange}
                onChange={(range) => setSelectionRange(range)}
              />
            )}
          </div>

          {/* Price Range */}
          <div className="space-y-2">
            <label className="block text-sm font-medium text-gray-600">Price (per night)</label>
            <div className="flex justify-between gap-2">
              <input
                type="number"
                value={minPrice}
                onChange={(e) => setMinPrice(Number(e.target.value))}
                placeholder="Min"
                className="w-1/2 px-2 py-1 border rounded-md border-gray-300 text-sm focus:outline-none"
              />
              <input
                type="number"
                value={maxPrice}
                onChange={(e) => setMaxPrice(Number(e.target.value))}
                placeholder="Max"
                className="w-1/2 px-2 py-1 border rounded-md border-gray-300 text-sm focus:outline-none"
              />
            </div>
          </div>

          {/* Occupancy */}
          <div className="space-y-2">
            <label className="block text-sm font-medium text-gray-600">Occupancy</label>
            <div className="grid grid-cols-2 gap-2">
            <div>
            <label className="text-gray-600 block mb-1">Adult</label>
              <input
                type="number"
                value={occupancy.adult}
                onChange={(e) =>
                  setOccupancy((prev) => ({ ...prev, adult: Number(e.target.value) }))
                }
                placeholder="Adults"
                className="w-full px-2 py-1 border rounded-md border-gray-300 text-sm focus:outline-none"
              />
              </div>
               <div>
               <label className="text-gray-600 block mb-1">Children</label>

              <input
                type="number"
                value={occupancy.children}
                onChange={(e) =>
                  setOccupancy((prev) => ({ ...prev, children: Number(e.target.value) }))
                }
                placeholder="Children"
                className="w-full px-2 py-1 border rounded-md border-gray-300 text-sm focus:outline-none"
              />
              </div>
               <div>
                <label className="text-gray-600 block mb-1">Room</label>

              <input
                type="number"
                value={occupancy.room}
                onChange={(e) =>
                  setOccupancy((prev) => ({ ...prev, room: Number(e.target.value) }))
                }
                placeholder="Rooms"
                className="w-full px-2 py-1 border rounded-md border-gray-300 text-sm focus:outline-none col-span-2"
              />
              </div>
            </div>
          </div>

          {/* Submit Button */}
          {/* <button
            type="button"
            onClick={onClickSearch}
            className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition text-sm"
          >
            Search
          </button> */}
        </form>
      </div>
    </div>
  );
}
