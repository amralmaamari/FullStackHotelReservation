import Featured from "../../components/hotel/Featured";
import Footer from "../../components/layout/Footer";
import Header from "../../components/layout/Header";
import Mail from "../../components/shared/Mail";
import PropertyList from "../../components/hotel/PropertyList";

export default function Home() {
  return (
    <>
      <Header />
      <Featured />
      <PropertyList />
      <Mail />
      <Footer />
    </>
  )
}
