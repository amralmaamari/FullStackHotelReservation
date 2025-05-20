import { Link } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import { useEffect, useRef, useState } from "react";

export default function Navbar() {
  const { user, logout } = useAuth();
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const wrapperRef = useRef(null);
  // Close dropdown on outside clicks
  useEffect(() => {
    function handleClickOutside(event) {
      if (
        dropdownOpen &&
        wrapperRef.current &&
        !wrapperRef.current.contains(event.target)
      ) {
        setDropdownOpen(false);
      }
    }
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [dropdownOpen]);
  return (
    <div className="bg-primary text-white">
      <div className="container mx-auto flex justify-between items-center px-4 py-3">
        <Link to="/" >
          <h1 className="font-extrabold text-2xl cursor-pointer">AmrSafari.com</h1>
        </Link>

        {!user ? (
          <div className="flex gap-3">
            <Link
              to="/auth"
              className="py-1 px-3 text-sm font-semibold bg-white text-primary border-2 border-white rounded hover:bg-primary hover:text-white transition"
            >
              Register
            </Link>
            <Link
              to="/auth"
              className="py-1 px-3 text-sm font-semibold bg-white text-primary border-2 border-white rounded hover:bg-primary hover:text-white transition"
            >
              Sign in
            </Link>
          </div>
        ) : (
          <div className="relative"  ref={wrapperRef}>
            <button onClick={() => setDropdownOpen(!dropdownOpen)} className="flex items-center gap-2 text-white text-sm">
               <img
              src="/public/avatar.png"
              alt="User Avatar"
              className="w-6 h-6 rounded-full"
            />
            </button>

            {dropdownOpen && (
              <div className="absolute right-0 mt-2 w-48 bg-white text-gray-800 rounded shadow-lg z-50">
                <div className="px-4 py-2 border-b text-sm font-medium">
                  {user.email}
                </div>
                <Link
                  to="/my-bookings"
                  className="block px-4 py-2 text-sm hover:bg-gray-100"
                  onClick={() => setDropdownOpen(false)}
                >
                  📋 My Bookings
                </Link>
                <button
                  onClick={() => {
                    logout();
                    setDropdownOpen(false);
                  }}
                  className="w-full text-left px-4 py-2 text-sm hover:bg-gray-100"
                >
                  🚪 Logout
                </button>
              </div>
            )}
          </div>
        )}
      </div>
    </div>
  );
}
