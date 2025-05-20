using Hotel_Business;
using HotelDataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet("getUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<UserDTO>> GetAllUsers()
        {
            List<UserDTO> userList = clsUsers.GetAllUsers();

            if (userList == null)
            {
                return NotFound(new { success = false, message = "There is no Users" });
            }
            return Ok(new { success = true, data = userList });
        }


        [HttpGet("{id}", Name = "getUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetUserById(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { success = false, message = $"Not accepted ID " });
            }


            var User = clsUsers.GetUsersInfoByID(id);

            if (User == null)
            {
                return NotFound(new { success = false, message = $"User with ID UserID not found." });
            }

            UserDTO UserDTO = User.userDTO;
            if (UserDTO != null) {
                return Ok(new { success = true, data = UserDTO });
                    }
            return Conflict(new { success = false, message = $"User with ID {id} could not be found due to a conflict." });



        }

        [HttpDelete("{id}", Name = "deleteUser")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteUser(int id)
        {
            // Validate the ID parameter
            if ((id < 1))
            {
                return BadRequest(new { success = false, message = "The ID cannot be null or empty." });
            }

            // Find the entity by ID
            var userDTO = clsUsers.GetUsersInfoByID(id);
            if (userDTO == null)
            {
                return NotFound(new { success = false, message = $"No user found with ID {id}" });
            }

            // Delete the entity
            if (clsUsers.DeleteUsers(id))
            {
                return Ok(new { success = true, message = $"User with ID {id} deleted successfully." });

            }
            return Conflict(new { success = false, message = $"User with ID {id} could not be deleted due to a conflict." });

        }



        [HttpPut("{id}", Name = "updateUser")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateUser(int id, [FromBody]  UserDTO userDTO)
        {
            // Validate the incoming DTO
            if (userDTO == null || id != userDTO.UserID)
            {
                return BadRequest(new { success = false, message = "User data is invalid." });
            }

            // Find the existing entity
            var existingUser = clsUsers.GetUsersInfoByID(userDTO.UserID);
            if (existingUser == null)
            {
                return NotFound(new { success = false, message = $"No User found with ID {userDTO.UserID}" });
            }

            // Update properties using reflection
            existingUser.Username = userDTO.Username;
            existingUser.Password = userDTO.Password;
            existingUser.CountryID = userDTO.CountryID  ;
            existingUser.Image = userDTO.Image;
            existingUser.City = userDTO.City;


            // Save the changes
            if (existingUser.Save())
            {
                return Ok(new { success = true, data = existingUser }); // Return the updated entity

            }

            return Conflict(new { success = false, message = $"User with ID {id} could not be update due to a conflict." });
        }

    }
}
