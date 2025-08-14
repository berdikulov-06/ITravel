using Microsoft.AspNetCore.Mvc;

namespace ITravel.Controllers
{
    public class TourController : Controller
    {
        [Route("Tour")]
        public IActionResult Tour()
        {
            return View();
        }
    }
}
