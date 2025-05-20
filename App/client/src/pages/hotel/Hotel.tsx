import { useParams } from "react-router-dom";
import { useState } from "react";
import Gallery from "../../components/imageSlide/Gallery";
import ReservationTypes from "../../components/reservation/ReservationTypes";
import Reserve from "../../components/reservation/Reserve";
import useFetch from "../../hooks/useFetch";

export default function Hotel() {
  const { id } = useParams<{ id: string }>();
  const { data, error, loading } = useFetch({ url: `/Hotel/${id}` });
  const imageList = data?.photos.map((photo) => photo.photoURL);
  const [openDialog, setOpenModal] = useState(false);

  const onClickClosePopUp = () => setOpenModal(false);
  const handleClickButton = () => setOpenModal(true);

  return (
    <div className="relative bg-gray-50 min-h-screen">
      {openDialog && <Reserve onClickClose={onClickClosePopUp} />}

      {/* Header */}
      <div className="bg-primary pt-1 px-4">
        <ReservationTypes />
      </div>

      {/* Loading */}
      {loading && (
        <div className="text-center py-10">
          <div className="animate-pulse text-lg text-gray-600">Loading hotel details...</div>
        </div>
      )}

      {/* Error */}
      {!loading && error && (
        <div className="text-center py-10">
          <div className="bg-red-100 text-red-600 py-3 px-4 rounded-md inline-block">
            {error || "Something went wrong."}
          </div>
        </div>
      )}

      {/* Content */}
      {!error && data && (
        <div className="container mx-auto py-6 px-4">
          {/* Hotel Title */}
          <h2 className="text-3xl font-extrabold text-gray-800 mb-4">{data.name}</h2>

          {/* Image Gallery */}
          <div className="mb-6">
            <Gallery images={imageList} />
          </div>

          {/* Main Info */}
          <div className="flex flex-col md:flex-row gap-6">
            {/* Left Side - Description */}
            <div className="md:w-2/3 bg-white p-4 rounded-md shadow-md">
              <h3 className="text-xl font-semibold text-gray-700 mb-3">Description</h3>
              <p className="text-gray-600 leading-relaxed text-sm md:text-base">{data.description}</p>
            </div>

            {/* Right Side - Pricing */}
            <div className="md:w-1/3 bg-blue-50 p-4 rounded-md shadow-md flex flex-col justify-between">
              <div>
                <h4 className="text-lg font-semibold text-blue-800 mb-2">💡 Property Highlights</h4>
                <p className="text-2xl font-bold text-blue-900">
                  ₹ {(data.cheapestPrice * 9).toLocaleString()}
                </p>
                <p className="text-gray-600 text-sm mb-3">(9 nights)</p>
              </div>
              <button
                onClick={handleClickButton}
                className="mt-4 w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 transition"
              >
                Reserve
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
