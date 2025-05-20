
export default function Loading() {
  return (
    <>
    {[1, 2, 3].map((_, i) => (
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
  )
}
