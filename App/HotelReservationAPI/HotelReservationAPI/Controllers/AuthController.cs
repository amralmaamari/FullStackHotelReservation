using HotelReservationAPI;
using Hotel_Business;
using HotelDataAccessLayer;
using HotelReservationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using HotelReservationDataLayer.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly clsJwtTokenService _jwtTokenService;
        public AuthController(clsJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register", Name = "register")]
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult RegisterUser([FromBody] SignUpDTO newUserDTO)
        {

            if (newUserDTO == null)
            {
                return BadRequest(new { success = false, message = "Invalid User data." });
            }
            if(clsUsers.GetUsersInfoByEmail(newUserDTO.Email) != null)
            {
                return BadRequest(new { success = false,  message = "This email address is already registered. Please use a different one."});

            }

            clsUsers user = clsUsers.FromSignUpDTO(newUserDTO);
            user.Save();

            if (user.UserID <= 0)
            {
                return BadRequest(new { success = false, message = "User could not be created." });
            }

            var token = _jwtTokenService.GenerateToken(user.Email);
            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            // return CreatedAtRoute("RegisterPerson", new { success = true, token });
            //return CreatedAtRoute("GetPersonById", new { id = user.UserID }, user);
            return Ok(new { success = true,  data = new
            {
                user.Username,
                user.Email,
                user.CountryID,
                user.Image,
                user.City
            }
            });


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
            //the below use http  
            

            if (await clsUsers.IsUserExists(loginDTO))
            {
                UserDTO user=clsUsers.GetUsersInfoByEmail(loginDTO.Email);
                var token = _jwtTokenService.GenerateToken(loginDTO.Email);
                Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        user.Username,
                        user.Email,
                        user.CountryID,
                        user.Image,
                        user.City
                    }
                });

                //return Ok(new { success = true, login= loginDTO });
            }

            return Unauthorized(new { success = false, message= "User not found!" });

        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return Ok(new { Success = true });
        }


    }
}
