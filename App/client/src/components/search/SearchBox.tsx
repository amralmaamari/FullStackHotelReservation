import { useEffect, useRef, useState } from "react";
import DatePicker from "../reservation/DatePicker";
import OccupancyCounter from "../reservation/OccupancyCounter";
import { useSearchContext } from "../../context/searchContext";
import { Link } from "react-router-dom";
import toast from "react-hot-toast";

export default function SearchBox() {
  
  const {destination, setDestination,selectionRange, setSelectionRange,occupancy, setOccupancy} = useSearchContext();
  const [openDate, setOpenDate] = useState<boolean>(false);
  const [openCounter, setOpenCounter] = useState<boolean>(false);
  
  const dateWrapperRef  =useRef<HTMLDivElement>(null);

 useEffect(() => {
  function handleClickOutside(event: MouseEvent) {
    if (
      openDate &&
      dateWrapperRef.current &&
      !dateWrapperRef.current.contains(event.target as Node)
    ) {
      setOpenDate(false);
    }
  }

  // 1) add on mount
  document.addEventListener("mousedown", handleClickOutside);

  // 2) cleanup on unmount or when openDate changes
  return () => {
    document.removeEventListener("mousedown", handleClickOutside);
  };
}, [openDate]);

  const handleOccupancy = (
    field: "adult" | "children" | "room",
    change: number
  ) => {
    setOccupancy((prev) => {
      return {
        ...prev,
        [field]:
          field === "adult"
            ? Math.max(prev[field] + change, 1)
            : Math.max(prev[field] + change, 0),
      };
    });
  };

  const handleOpenDatePicker =()=>{
    setOpenDate(!openDate);
    setOpenCounter(false);
  }
  const handleOpenCounter =()=>{
    setOpenCounter(!openCounter);
    setOpenDate(false);
  }
  const handleBookingButton = () => {
    const { startDate, endDate } = selectionRange;
  
    if (!startDate || !endDate) {
      toast.error("❗ Please select both check-in and check-out dates.");
      return false;
    }
  
    if (startDate.getTime() === endDate.getTime()) {
      toast.error("❗ Check-out date must be after check-in date.");
      return false;
    }
  
    if (endDate.getTime() < startDate.getTime()) {
      toast.error("❗ Invalid date range. Please check your selection.");
      return false;
    }
  
    return true; // ✅ Passed all checks
  };
  
  return (
    <div className="relative  z-10 -mt-6 px-4">
      <div className="container mx-auto bg-yellow-100 border border-yellow-300 p-4 rounded-xl shadow-lg">
        <form className="flex flex-col   lg:flex-row lg:flex-1 gap-2 items-start">
          <div className="flex flex-1 items-center w-full lg:w-auto border border-gray-300 rounded-lg overflow-hidden px-3 py-2 shadow-sm  bg-white">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="w-5 h-5"
              viewBox="0 0 24 24"
              width="50px"
            >
              <path d="M2.75 12h18.5c.69 0 1.25.56 1.25 1.25V18l.75-.75H.75l.75.75v-4.75c0-.69.56-1.25 1.25-1.25m0-1.5A2.75 2.75 0 0 0 0 13.25V18c0 .414.336.75.75.75h22.5A.75.75 0 0 0 24 18v-4.75a2.75 2.75 0 0 0-2.75-2.75zM0 18v3a.75.75 0 0 0 1.5 0v-3A.75.75 0 0 0 0 18m22.5 0v3a.75.75 0 0 0 1.5 0v-3a.75.75 0 0 0-1.5 0m-.75-6.75V4.5a2.25 2.25 0 0 0-2.25-2.25h-15A2.25 2.25 0 0 0 2.25 4.5v6.75a.75.75 0 0 0 1.5 0V4.5a.75.75 0 0 1 .75-.75h15a.75.75 0 0 1 .75.75v6.75a.75.75 0 0 0 1.5 0m-13.25-3h7a.25.25 0 0 1 .25.25v2.75l.75-.75h-9l.75.75V8.5a.25.25 0 0 1 .25-.25m0-1.5A1.75 1.75 0 0 0 6.75 8.5v2.75c0 .414.336.75.75.75h9a.75.75 0 0 0 .75-.75V8.5a1.75 1.75 0 0 0-1.75-1.75z"></path>
            </svg>
            <input
              type="text"
              value={destination}
              onChange={(e)=>setDestination(e.target.value)}
              placeholder="Enter your destination"
              className="ml-3 outline-none w-full bg-transparent text-gray-700 placeholder-gray-400"
            />
          </div>

          <div className="relative flex-1 z-10 w-full lg:w-auto"  ref={dateWrapperRef}>
            <div
              className="flex items-center border border-gray-300 rounded-lg overflow-hidden px-3 py-2 shadow-sm  bg-white cursor-pointer "
              onClick={handleOpenDatePicker}
             
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="w-5 h-5"
                viewBox="0 0 24 24"
                width="50px"
              >
                <path d="M22.5 13.5v8.25a.75.75 0 0 1-.75.75H2.25a.75.75 0 0 1-.75-.75V5.25a.75.75 0 0 1 .75-.75h19.5a.75.75 0 0 1 .75.75zm1.5 0V5.25A2.25 2.25 0 0 0 21.75 3H2.25A2.25 2.25 0 0 0 0 5.25v16.5A2.25 2.25 0 0 0 2.25 24h19.5A2.25 2.25 0 0 0 24 21.75zm-23.25-3h22.5a.75.75 0 0 0 0-1.5H.75a.75.75 0 0 0 0 1.5M7.5 6V.75a.75.75 0 0 0-1.5 0V6a.75.75 0 0 0 1.5 0M18 6V.75a.75.75 0 0 0-1.5 0V6A.75.75 0 0 0 18 6M5.095 14.03a.75.75 0 1 0 1.06-1.06.75.75 0 0 0-1.06 1.06m.53-1.28a1.125 1.125 0 1 0 0 2.25 1.125 1.125 0 0 0 0-2.25.75.75 0 0 0 0 1.5.375.375 0 1 1 0-.75.375.375 0 0 1 0 .75.75.75 0 0 0 0-1.5m-.53 6.53a.75.75 0 1 0 1.06-1.06.75.75 0 0 0-1.06 1.06m.53-1.28a1.125 1.125 0 1 0 0 2.25 1.125 1.125 0 0 0 0-2.25.75.75 0 0 0 0 1.5.375.375 0 1 1 0-.75.375.375 0 0 1 0 .75.75.75 0 0 0 0-1.5m5.845-3.97a.75.75 0 1 0 1.06-1.06.75.75 0 0 0-1.06 1.06m.53-1.28A1.125 1.125 0 1 0 12 15a1.125 1.125 0 0 0 0-2.25.75.75 0 0 0 0 1.5.375.375 0 1 1 0-.75.375.375 0 0 1 0 .75.75.75 0 0 0 0-1.5m-.53 6.53a.75.75 0 1 0 1.06-1.06.75.75 0 0 0-1.06 1.06M12 18a1.125 1.125 0 1 0 0 2.25A1.125 1.125 0 0 0 12 18a.75.75 0 0 0 0 1.5.375.375 0 1 1 0-.75.375.375 0 0 1 0 .75.75.75 0 0 0 0-1.5m5.845-3.97a.75.75 0 1 0 1.06-1.06.75.75 0 0 0-1.06 1.06m.53-1.28a1.125 1.125 0 1 0 0 2.25 1.125 1.125 0 0 0 0-2.25.75.75 0 0 0 0 1.5.375.375 0 1 1 0-.75.375.375 0 0 1 0 .75.75.75 0 0 0 0-1.5m-.53 6.53a.75.75 0 1 0 1.06-1.06.75.75 0 0 0-1.06 1.06m.53-1.28a1.125 1.125 0 1 0 0 2.25 1.125 1.125 0 0 0 0-2.25.75.75 0 0 0 0 1.5.375.375 0 1 1 0-.75.375.375 0 0 1 0 .75.75.75 0 0 0 0-1.5"></path>
              </svg>
              <button type="button" className="ml-3">
              {`${selectionRange.startDate.toLocaleDateString()} — ${selectionRange.endDate.toLocaleDateString()}`}
              </button>
            </div>
            {openDate && <DatePicker
          selectionRange={selectionRange}
          onChange={(range) => setSelectionRange(range)}
        />}
          </div>

          {/* Counter */}
          <div className="relative flex-1  z-10 w-full lg:w-auto cursor-pointer" >
            <div className="flex items-center w-full cursor-pointer lg:w-auto border border-gray-300 rounded-lg overflow-hidden px-3 py-2 shadow-sm  bg-white">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="black"
                viewBox="0 0 24 24"
                width="20px"
              >
                <path d="M16.5 6a4.5 4.5 0 1 1-9 0 4.5 4.5 0 0 1 9 0M18 6A6 6 0 1 0 6 6a6 6 0 0 0 12 0M3 23.25a9 9 0 1 1 18 0 .75.75 0 0 0 1.5 0c0-5.799-4.701-10.5-10.5-10.5S1.5 17.451 1.5 23.25a.75.75 0 0 0 1.5 0"></path>
              </svg>
              <div className="occupancy-config" onClick={handleOpenCounter}>
                <span>{occupancy.adult} adult· </span>
                <span>{occupancy.children} children· </span>
                <span>{occupancy.room} room </span>
              </div>
            </div>
          
            <div className={`occupancy-popup ${openCounter?"block":"hidden"}   absolute top-[50px] w-full   p-3 bg-white rounded-md`}>
              <OccupancyCounter
                label="Adults"
                value={occupancy.adult}
                onIncrement={() => handleOccupancy("adult", 1)}
                onDecrement={() => handleOccupancy("adult", -1)}
              />
              <OccupancyCounter
                label="Children"
                value={occupancy.children}
                onIncrement={() => handleOccupancy("children", 1)}
                onDecrement={() => handleOccupancy("children", -1)}
              />
              <OccupancyCounter
                label="Room"
                value={occupancy.room}
                onIncrement={() => handleOccupancy("room", 1)}
                onDecrement={() => handleOccupancy("room", -1)}
              />
            </div>
          </div>

          <Link
  to="/hotels"
  className="primary-btn w-full lg:w-fit"
  onClick={(e) => {
    const valid = handleBookingButton();
    if (!valid) e.preventDefault(); // ⛔ منع الانتقال
  }}
>
  📅 Book Yours
</Link>

        </form>
      </div>
    </div>
  );
}
