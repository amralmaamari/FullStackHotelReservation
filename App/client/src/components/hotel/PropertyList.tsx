import { Link } from "react-router-dom";
import useFetch from "../../hooks/useFetch";

export default function PropertyList() {
  const { data, error, loading } = useFetch({ url: '/Hotel/countByType' });

  return (
    <div className="property-list container mx-auto grid grid-cols-2 lg:grid-cols-4 gap-4 px-4 py-3">
      {loading && (
  <>
    {[1, 2, 3, 4].map((_, i) => (
      <div
        key={i}
        className="animate-pulse bg-gray-200 rounded-md overflow-hidden h-[220px]"
      >
        <div className="h-2/3 bg-gray-300 w-full"></div>
        <div className="p-2 space-y-2">
          <div className="h-4 bg-gray-300 rounded w-3/4"></div>
          <div className="h-3 bg-gray-300 rounded w-1/2"></div>
        </div>
      </div>
    ))}
  </>
)}

{!loading && error && (
  <div className="col-span-full flex items-center gap-3 bg-red-100 border border-red-300 text-red-700 px-4 py-3 rounded-md">
    <svg
      xmlns="http://www.w3.org/2000/svg"
      className="h-5 w-5 text-red-500"
      viewBox="0 0 20 20"
      fill="currentColor"
    >
      <path
        fillRule="evenodd"
        d="M18 10A8 8 0 11 2 10a8 8 0 0116 0zm-8.75-3.75a.75.75 0 011.5 0v3.5a.75.75 0 01-1.5 0v-3.5zm.75 7a.875.875 0 100-1.75.875.875 0 000 1.75z"
        clipRule="evenodd"
      />
    </svg>
    <div>
      <p className="font-semibold">Oops! Something went wrong.</p>
      <p className="text-sm">{error || "Unable to load data. Please try again later."}</p>
    </div>
  </div>
)}
      {data && data.map((item) => {
  const property = PropertyListArray.find(prop =>
    prop.name.toLowerCase().includes(item.type.toLowerCase())
  );
  if (!property) return null;

  return (
    <div
      key={item.type}
      className={`${item.type} cursor-pointer group bg-white rounded-lg shadow-md hover:shadow-xl transition duration-300 overflow-hidden`}
    >
      <Link to={property.url}>
        <div className="relative">
          <img
            src={property.img}
            alt={item.name}
            className="w-full h-40 sm:h-48 object-cover transition-transform duration-300 group-hover:scale-105"
          />
          <div className="absolute bottom-0 left-0 w-full bg-gradient-to-t from-black/60 to-transparent p-2 text-white">
            <h3 className="text-lg font-semibold leading-tight">{item.name}</h3>
          </div>
        </div>
        <div className="p-3 text-center">
          <span className="block text-[15px] text-gray-600 md:text-[18px] font-medium">
            {item.count} {item.type}
          </span>
        </div>
      </Link>
    </div>
  );
})}

    </div>
  );
}

interface PropertyListArrayProps {
  name: string;
  img: string;
  url: string;
}

const PropertyListArray: PropertyListArrayProps[] = [
  {
    name: "Hotels",
    img: "https://r-xx.bstatic.com/xdata/images/hotel/263x210/595550862.jpeg?k=3514aa4abb76a6d19df104cb307b78b841ac0676967f24f4b860d289d55d3964&o=",
    url: "/hotels",
  },
  {
    name: "Apartments",
    img: "https://r-xx.bstatic.com/xdata/images/hotel/263x210/595548591.jpeg?k=01741bc3aef1a5233dd33794dda397083092c0215b153915f27ea489468e57a2&o=",
    url: "/apartments",
  },
  {
    name: "Resorts",
    img: "https://q-xx.bstatic.com/xdata/images/hotel/263x210/595551044.jpeg?k=262826efe8e21a0868105c01bf7113ed94de28492ee370f4225f00d1de0c6c44&o=",
    url: "/resorts",
  },
  {
    name: "Villas",
    img: "https://q-xx.bstatic.com/xdata/images/hotel/263x210/620168315.jpeg?k=300d8d8059c8c5426ea81f65a30a7f93af09d377d4d8570bda1bd1f0c8f0767f&o=",
    url: "/villas",
  },
];
