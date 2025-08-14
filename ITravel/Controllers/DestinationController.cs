using Microsoft.AspNetCore.Mvc;

namespace ITravel.Controllers
{
    public class DestinationController : Controller
    {
        [Route("Destination")]
        public IActionResult Destination() 
        { 
            return View(); 
        }

    }
}
