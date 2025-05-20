import React from "react";

interface LightboxProps {
  images: string[];
  currentIndex: number;
  onClose: () => void;
  onNext: () => void;
  onPrev: () => void;
}

export const Lightbox: React.FC<LightboxProps> = ({
  images,
  currentIndex,
  onClose,
  onNext,
  onPrev,
}) => {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-90">
    {/* Close Button */}
    <button
      onClick={onClose}
      className="absolute top-5 right-5 text-white bg-red-600 hover:bg-red-700 rounded-full w-10 h-10 flex items-center justify-center text-2xl shadow-lg"
      title="Close"
    >
      ×
    </button>
  
    {/* Prev Button */}
    <button
      onClick={onPrev}
      className="absolute left-5 text-white bg-gray-800 hover:bg-gray-700 rounded-full w-10 h-10 flex items-center justify-center text-xl shadow-lg"
      title="Previous"
    >
      ‹
    </button>
  
    {/* Image */}
    <img
      src={images[currentIndex]}
      alt={`Slide ${currentIndex}`}
      className="max-w-[90%] max-h-[80%] rounded-md shadow-2xl object-contain border-4 border-white"
    />
  
    {/* Next Button */}
    <button
      onClick={onNext}
      className="absolute right-5 text-white bg-gray-800 hover:bg-gray-700 rounded-full w-10 h-10 flex items-center justify-center text-xl shadow-lg"
      title="Next"
    >
      ›
    </button>
  </div>
  
  );
};
