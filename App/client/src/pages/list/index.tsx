import HotelBox from "../../components/hotel/HotelBox";
import { useSearchContext } from "../../context/searchContext";
import ReservationTypes from "../../components/reservation/ReservationTypes";
import SideSearchBox from "../../components/search/SideSearchBox";
import useFetch from "../../hooks/useFetch";
import { IHotel } from "../../interface/Interface";
import Loading from "../../components/response/Loading";
import Error from "../../components/response/Error";

export default function List() {


  const { minPrice, maxPrice, destination } = useSearchContext();
  
  const { data, error, loading, reFetch } = useFetch({
    url: `/Hotel/hotels?min=${minPrice || 0}&destination=${
      destination || ""
    }&max=${maxPrice || 500}&limit=6`,
  });

  const handleClickSearch = () => {
    reFetch();
  };
  return (
    <>
      <div className="bg-primary pt-1 px-4">
       
        <ReservationTypes />
      </div>

      <div className="container mx-auto flex flex-col lg:flex-row gap-3 px-4  py-3 ">
        <SideSearchBox onClickSearch={handleClickSearch} />
        {/* Loading */}
        <div className="flex flex-col gap-3 lg:w-2/3">
          {loading && (
           <Loading />
          )}

          {/* Erro Message */}
          {!loading && error && (
            <Error error={error} />
          )}
          {!error &&
            data &&
            data.map((hotel: IHotel) => {
              return <HotelBox hotel={hotel} key={hotel.hotelID} />;
            })}
        </div>
      </div>
    </>
  );
}
