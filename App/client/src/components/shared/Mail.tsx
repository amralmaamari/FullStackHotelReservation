
export default function Mail() {
  return (
    <div className="text-center py-11 bg-primary text-white px-4">
        <h2 className="text-4xl font-extrabold mb-3">Save time, save money!</h2>
        <p className="mb-3">Sign up and we'll send the best deals to you</p>

        <div className="flex flex-col md:flex-row  justify-center gap-3 px-4">
            <input type="email"  placeholder="Your Email!" className=" p-2 rounded-sm text-black md:w-1/3 "/>
            <button className="primary-btn " >Subscribe</button>
            </div>
    </div>
  )
}
