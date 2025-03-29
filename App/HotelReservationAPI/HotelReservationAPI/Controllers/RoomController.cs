using System.Threading.Tasks;
using Hotel_Business;
using HotelDataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddNewRoom([FromBody] RoomDTO newRoomDTO)
        {

            if (newRoomDTO == null)
            {
                return BadRequest(new { success = false, message = "Invalid room data." });
            }

            clsRooms room = new clsRooms(newRoomDTO, clsRooms.enMode.AddNew);
            room.Save();

            if (room.HotelID <= 0)
            {
                return BadRequest(new { success = false, message = "Room could not be created." });
            }

            //var token = _jwtTokenService.GenerateToken(person.PersonID);

            // return CreatedAtRoute("RegisterPerson", new { success = true, token });
            //return CreatedAtRoute("GetPersonById", new { id = user.UserID }, user);
            return Ok(new { success = true, data = room });


        }


        [HttpGet("getRooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<RoomDTO>> GetAllUsers()
        {
            List<RoomDTO> roomList = clsRooms.GetAllRooms();

            if (roomList == null)
            {
                return NotFound(new { success = true, message = "There is no Rooms" });
            }
            return Ok(new { success = true, data = roomList });
        }


        [HttpGet("{id}", Name = "getRoomById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetRoomById(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { success = false, message = $"Not accepted ID " });
            }


            var room = clsRooms.GetRoomsInfoByID(id);

            if (room == null)
            {
                return NotFound(new { success = false, message = $"Room with ID RoomID not found." });
            }

            RoomDTO RoomDTO = room.roomsDTO;
            if (RoomDTO != null)
            {
                return Ok(new { success = true, data = RoomDTO });
            }
            return Conflict(new { success = false, message = $"Room with ID {id} could not be found due to a conflict." });



        }


        [HttpDelete("{id}", Name = "deleteRoom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteRoom(int id)
        {
            // Validate the ID parameter
            if ((id < 1))
            {
                return BadRequest(new { success = false, message = "The ID cannot be null or empty." });
            }

            // Find the entity by ID
            var roomDTO = clsRooms.GetRoomsInfoByID(id);
            if (roomDTO == null)
            {
                return NotFound(new { success = false, message = $"No room found with ID {id}" });
            }

            // Delete the entity
            if (clsRooms.DeleteRooms(id))
            {
                return Ok(new { success = true, message = $"Room with ID {id} deleted successfully." });

            }
            return Conflict(new { success = false, message = $"Room with ID {id} could not be deleted due to a conflict." });

        }



        [HttpPut("{id}", Name = "updateRoom")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateRoom(int id, [FromBody] RoomDTO roomDTO)
        {
            // Validate the incoming DTO
            if (roomDTO == null || id != roomDTO.RoomID)
            {
                return BadRequest(new { success = false, message = "Room data is invalid." });
            }

            // Find the existing entity
            var existingRoom = clsRooms.GetRoomsInfoByID(roomDTO.RoomID);
            if (existingRoom == null)
            {
                return NotFound(new { success = false, message = $"No Room found with ID {roomDTO.RoomID}" });
            }

            // Update properties using reflection
            existingRoom.RoomTypeID = roomDTO.RoomTypeID;
            existingRoom.RoomNumber = roomDTO.RoomNumber;
            existingRoom.Price = roomDTO.Price;


            // Save the changes
            if (existingRoom.Save())
            {
                return Ok(new { success = true, data = existingRoom }); // Return the updated entity

            }

            return Conflict(new { success = false, message = $"Room with ID {id} could not be update due to a conflict." });
        }


        //id=RoomID
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddRoomAvailability(int id,[FromBody] RoomUnavailableDatesDTO newroomUnavailableDatesDTO)
        {
            if ((id < 1))
            {
                return BadRequest(new { success = false, message = "The ID cannot be null or empty." });
            }

            if (newroomUnavailableDatesDTO == null || newroomUnavailableDatesDTO.RoomID != id)
            {
                return BadRequest(new { success = false, message = "Invalid room data." });
            }

            if (newroomUnavailableDatesDTO.CheckIn >= newroomUnavailableDatesDTO.CheckOut)
            {
                return BadRequest(new { success = false, message = "Check-in must be before check-out." });
                
            }


            //here i have to check if the room have active dates 
            bool available = await clsRoomUnavailableDates.IsRoomAvailableBetweenDates(newroomUnavailableDatesDTO.RoomID, newroomUnavailableDatesDTO.CheckIn, newroomUnavailableDatesDTO.CheckOut);
            if (!available) 
            {
                return NotFound(new { success = false, message = "The Room is not Available between the Dates" });

            }

            clsRoomUnavailableDates roomUnavailableDates = new clsRoomUnavailableDates(newroomUnavailableDatesDTO, clsRoomUnavailableDates.enMode.AddNew);
            roomUnavailableDates.Save();

            if (roomUnavailableDates.RoomUnavailableDateID <= 0)
            {
                return BadRequest(new { success = false, message = "RoomUnavailableDates could not be created." });
            }

           
            return Ok(new { success = true, data = roomUnavailableDates });


        }

    }
}
