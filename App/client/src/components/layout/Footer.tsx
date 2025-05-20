
export default function Footer() {
  return (
    <footer className="container mx-auto  grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-3 px-4 bg-bgPrimary py-2 ">
    {FooterList.map((item, index) => (
      <div key={index}>
        <h1 className="font-extrabold text-2xl mb-2">{item.title}</h1>
        <ul>
          {item.items.map((i, idx) => (
            <li key={idx} className="mb-1">
              <a className="cursor-pointer underline">{i}</a>
            </li>
          ))}
        </ul>
      </div>
    ))}
  </footer>
  )
}

const FooterList=
    [
        {
          "title": "Support",
          "items": [
            "Coronavirus (COVID-19) FAQs",
            "Manage your trips",
            "Contact Customer Service",
            "Safety Resource Center"
          ]
        },        
        {
          "title": "Terms and settings",
          "items": [
            "Privacy & cookies",
            "Terms and Conditions",
            "Grievance officer",
            "Modern Slavery Statement",
            "Human Rights Statement"
          ]
        },
        {
          "title": "Partners",
          "items": [
            "Extranet login",
            "Partner help",
            "List your property",
            "Become an affiliate"
          ]
        },
        {
          "title": "About",
          "items": [
            "How We Work",
            "Sustainability",
            "Press center",
            "Careers",
            "Investor relations",
            "Corporate contact"
          ]
        },
        {
          "title": "Discover",
          "items": [
            "Genius loyalty program",
            "Seasonal and holiday deals",
            "Travel articles",
            "Booking.com for Business",
            "Traveler Review Awards",
            "Car rental",
            "Flight finder",
            "Restaurant reservations",
            "Booking.com for Travel Agents"
          ]
        }
      ];
      

