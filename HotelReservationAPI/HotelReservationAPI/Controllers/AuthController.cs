using Hotel_Business;
using HotelDataAccessLayer;
using HotelReservationAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        [HttpPost("register", Name = "register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult RegisterUser([FromBody] UserDTO newUserDTO)
        {

            if (newUserDTO == null)
            {
                return BadRequest(new { success = false, message = "Invalid User data." });
            }

            clsUsers user = new clsUsers(newUserDTO, clsUsers.enMode.AddNew);
            user.Save();

            if (user.UserID <= 0)
            {
                return BadRequest(new { success = false, message = "User could not be created." });
            }

            //var token = _jwtTokenService.GenerateToken(person.PersonID);

            // return CreatedAtRoute("RegisterPerson", new { success = true, token });
            //return CreatedAtRoute("GetPersonById", new { id = user.UserID }, user);
            return Ok(new { success = true,  user });


        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {

            if (loginDTO == null)
            {
                return BadRequest(new { success = false, message = "Invalid User data." });
            }

            // Validate user credentials against the database
            if (await clsUsers.IsUserExists(loginDTO))
            {
                //var token = _jwtTokenService.GenerateToken(loginDTO.Email);
                //return Ok(new { success = true, token });

                return Ok(new { success = true, login= loginDTO });
            }

            return Unauthorized(new { success = false, message= "User not found!" });

        }


    }
}
