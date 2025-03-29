using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        // GET: api/Home/Check
        [HttpGet("Check")]
        public IActionResult Check()
        {
            return Ok("API is working!");
        }
    }
}
