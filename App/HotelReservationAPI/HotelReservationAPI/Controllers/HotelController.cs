using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel_Business;
using HotelDataAccessLayer;
using HotelReservationDataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddNewHotel([FromBody] HotelDTO newHotelDTO)
        {

            if (newHotelDTO == null)
            {
                return BadRequest(new { success = false, message = "Invalid Hotel data." });
            }

            clsHotels hotel = new clsHotels(newHotelDTO, clsHotels.enMode.AddNew);
            hotel.Save();

            if (hotel.HotelID <= 0)
            {
                return BadRequest(new { success = false, message = "Hotel could not be created." });
            }

            //var token = _jwtTokenService.GenerateToken(person.PersonID);

            // return CreatedAtRoute("RegisterPerson", new { success = true, token });
            //return CreatedAtRoute("GetPersonById", new { id = user.UserID }, user);
            return Ok(new { success = true, data= hotel });


        }


        [HttpPut("{id}", Name = "updateHotel")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateHotel(int id, [FromBody] HotelDTO hotelDTO)
        {
            // Validate the incoming DTO
            if (hotelDTO == null || id != hotelDTO.HotelID)
            {
                return BadRequest(new { success = false, message = "Hotel data is invalid." });
            }

            // Find the existing entity
            var existingHotel = clsHotels.GetHotelsInfoByID(hotelDTO.HotelID);
            if (existingHotel == null)
            {
                return NotFound(new { success = false, message = $"No Hotel found with ID {hotelDTO.HotelID}" });
            }

            // Update properties using reflection
            existingHotel.Name = hotelDTO.Name;
            existingHotel.HotelTypeID = hotelDTO.HotelTypeID;
            existingHotel.City = hotelDTO.City;
            existingHotel.Address = hotelDTO.Address;
            existingHotel.Distance = hotelDTO.Distance;
            existingHotel.Title = hotelDTO.Title;
            existingHotel.Description = hotelDTO.Description;
            existingHotel.Rating = hotelDTO.Rating;
            existingHotel.Featured = hotelDTO.Featured;


            // Save the changes
            if (existingHotel.Save())
            {
                return Ok(new { success = true, data = existingHotel }); // Return the updated entity

            }

            return Conflict(new { success = false, message = $"Hotel with ID {id} could not be update due to a conflict." });
        }


        [HttpDelete("{id}", Name = "deleteHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteHotel(int id)
        {
            // Validate the ID parameter
            if ((id < 1))
            {
                return BadRequest(new { success = false, message = "The ID cannot be null or empty." });
            }

            // Find the entity by ID
            var hotelDTO = clsHotels.GetHotelsInfoByID(id);
            if (hotelDTO == null)
            {
                return NotFound(new { success = false, message = $"No hotel found with ID {id}" });
            }

            // Delete the entity
            if (clsHotels.DeleteHotels(id))
            {
                return Ok(new { success = true, message = $"Hotel with ID {id} deleted successfully." });

            }
            return Conflict(new { success = false, message = $"Hotel with ID {id} could not be deleted due to a conflict." });

        }

    
        [HttpGet("hotels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<HotelDetailsDTO>> GetAllHotels(
            [FromQuery] decimal min = 0,
            [FromQuery] string destination = "",
            [FromQuery] decimal max = 999,
            [FromQuery] int limit = 6)
        {
            var hotelList = clsHotels.GetAllHotelsDetailsParameters(limit, destination, min, max);

            if (hotelList == null || hotelList.Count == 0)
            {
                return NotFound(new { success = false, message = "No hotels found within the given filters." });

            }
            return Ok(new { success = true, data = hotelList });
        }



        [HttpGet("{id}", Name = "getHotelById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetHotelById(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { success = false, message = $"Not accepted ID " });
            }


            var hotelsDetailsDTO = clsHotels.GetHotelDetailsByID(id);

            if (hotelsDetailsDTO == null)
            {
                return NotFound(new { success = false, message = $"Hotel with ID HotelID not found." });
            }

            
            if (hotelsDetailsDTO.HotelID != 0)
            {
                return Ok(new { success = true, data = hotelsDetailsDTO });
            }
            return Conflict(new { success = false, message = $"Hotel with ID {id} could not be found due to a conflict." });

        }


        [HttpGet("countByCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CountHotelsByCity([FromQuery] string cities)
        {
            if (string.IsNullOrWhiteSpace(cities))
            {
                return BadRequest(new { success = false, message = "Cities parameter is required." });
            }

            string[] cityArray = cities.Split(',');

            try
            {
                var result = new List<int>();
                foreach (var city in cityArray)
                {
                    int count = await clsHotels.CountHotelsByCity(city.Trim());
                    result.Add(count);
                }
                return Ok(new { success = true, data = result });

            }
            catch (Exception ex) {
                return StatusCode(500, new { success = false, message = ex.Message });

            }

        }



        [HttpGet("countByType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CountHotelsByType()
        {
           


            int hotelCount = await clsHotels.CountHotelsByType("Hotel");
            int apartmentCount = await clsHotels.CountHotelsByType("Apartment");
            int resortCount = await clsHotels.CountHotelsByType("Resort");
            int villaCount = await clsHotels.CountHotelsByType("Villa");
            int cabinCount = await clsHotels.CountHotelsByType("Cabin");

            var result = new List<Object> {
                new { type = "Hotel", count = hotelCount },
                new { type = "Apartment", count = apartmentCount },
                new { type = "Resort", count = resortCount },
                new { type = "Villa", count = villaCount },
                new { type = "Cabin", count = cabinCount }
            };

            if(result == null)
            {
                return NotFound(new { success = false, message = "Sorry, we couldn't find any results." });

            }

            return Ok(new { success = true, data = result });



            

        }

        [HttpGet("rooms/details", Name = "getAllRoomsOfHotelID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult GetAllRoomsOfHotelID( int id)
        {
            if (id < 1)
            {
                return BadRequest(new { success = false, message = $"Not accepted ID " });
            }

            var roomList = clsRooms.GetAllRoomsOfHotelID(id);

            if (roomList == null || roomList.Count == 0)
            {
                return NotFound(new { success = false, message = "There is no rooms" });

            }
            return Ok(new { success = true, data = roomList });
        }
    }
}
