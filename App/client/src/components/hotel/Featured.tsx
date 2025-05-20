import useFetch from "../../hooks/useFetch";

export default function Featured() {
  const { data, error, loading } = useFetch({ url: '/Hotel/countByCity?cities=Berlin%2CMadrid%2CLondon' });

  return (
    <div className="container mx-auto px-4 py-6">
      <h2 className="text-2xl font-bold mb-4">Featured Cities</h2>

      {loading && (
        <div className="text-center text-gray-500 py-6">
          <span className="animate-spin inline-block w-6 h-6 border-4 border-blue-500 border-t-transparent rounded-full" />
          <p className="mt-2">Loading featured destinations...</p>
        </div>
      )}

      {!loading && error && (
        <div className="text-center text-red-600 bg-red-100 py-3 px-4 rounded-md">
          Error: {error}
        </div>
      )}

      {!loading && data && (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
          {/* Berlin */}
          <FeaturedCard
            city="Berlin"
            count={data[0]}
            img="https://cf.bstatic.com/xdata/images/city/max500/957801.webp?k=a969e39bcd40cdcc21786ba92826063e3cb09bf307bcfeac2aa392b838e9b7a5&o="
          />
          {/* Madrid */}
          <FeaturedCard
            city="Madrid"
            count={data[1]}
            img="https://cf.bstatic.com/xdata/images/city/max500/690334.webp?k=b99df435f06a15a1568ddd5f55d239507c0156985577681ab91274f917af6dbb&o="
          />
          {/* London */}
          <FeaturedCard
            city="London"
            count={data[2]}
            img="https://cf.bstatic.com/xdata/images/city/max500/689422.webp?k=2595c93e7e067b9ba95f90713f80ba6e5fa88a66e6e55600bd27a5128808fdf2&o="
          />
        </div>
      )}
    </div>
  );
}

interface CardProps {
  city: string;
  count: number;
  img: string;
}

function FeaturedCard({ city, count, img }: CardProps) {
  return (
    <div className="relative overflow-hidden rounded-lg shadow-md group cursor-pointer">
      <img
        src={img}
        alt={city}
        className="w-full h-64 object-cover transform group-hover:scale-110 transition duration-500"
      />
      <div className="absolute bottom-0 left-0 right-0 bg-gradient-to-t from-black/80 to-transparent p-4">
        <h1 className="text-2xl font-bold text-white">{city}</h1>
        <h2 className="text-white text-lg">{count} properties</h2>
      </div>
    </div>
  );
}
