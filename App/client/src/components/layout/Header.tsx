import ReservationTypes from "../reservation/ReservationTypes";
import SearchBox from "../search/SearchBox";
import  "../../App.css";
export default function Header() {
    const IMAGE_URL="https://q-xx.bstatic.com/xdata/images/xphoto/2880x868/476352618.jpeg?k=59fbbc84451a0f918590e8ab25268f08cfa70480b2739238f7f5830fc523c53a&o=";

  return (
    <>
        <div className="bg-primary pt-1 px-4">
            
            <ReservationTypes />
        </div>

        <div className="bg-cover bg-center h-[300px] w-full  relative px-4 " style={{ backgroundImage: `url(${IMAGE_URL})`,backgroundPosition:"center" }}  >  
            <div  className="container mx-auto  text-onPrimary  pt-2 md:pt-9">
                <h2 className="text-3xl mb-3 md:text-5xl font-extrabold">Live the dream in a <br /> vacation home </h2>
                <p className="text-1xl md:text-2xl">Choose from houses, villas, cabins, and more</p>
                <button className="primary-btn mt-2" >Book yours</button>
                
            </div>      
            
        </div>

        <SearchBox />
    </>
  )
}
