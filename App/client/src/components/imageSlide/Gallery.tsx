import  { useState } from "react";
import { Lightbox } from "./Lightbox";

interface IGalleryProps{
  images:string[],
}
export default function Gallery({images}:IGalleryProps) {
  

  const [lightboxOpen, setLightboxOpen] = useState<boolean>(false);
  const [currentIndex, setCurrentIndex] = useState<number>(0);

  const openLightbox = (index: number) => {
    setCurrentIndex(index);
    setLightboxOpen(true);
  };

  const closeLightbox = () => {
    setLightboxOpen(false);
  };

  const nextSlide = () => {
    setCurrentIndex((prevIndex) => (prevIndex + 1) % images.length);
  };

  const prevSlide = () => {
    setCurrentIndex((prevIndex) => (prevIndex - 1 + images.length) % images.length);
  };

  return (
    <div className="">
    <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-5 gap-3">
  {images.map((img, idx) => (
    <div
      key={idx}
      className="relative group overflow-hidden rounded-lg shadow-md cursor-pointer"
      onClick={() => openLightbox(idx)}
    >
      <img
        src={img}
        alt={`Thumbnail ${idx + 1}`}
        className="w-full h-[150px] sm:h-[180px] object-cover transform group-hover:scale-105 transition duration-300 ease-in-out"
      />
      <div className="absolute inset-0 bg-black bg-opacity-0 group-hover:bg-opacity-10 transition" />
    </div>
  ))}
</div>


   
    {lightboxOpen && (
      <Lightbox
        images={images}
        currentIndex={currentIndex}
        onClose={closeLightbox}
        onNext={nextSlide}
        onPrev={prevSlide}
      />
    )}
  </div>
  )
}
