using Microsoft.AspNetCore.Mvc;

namespace ITravel.Controllers
{
    public class AboutController : Controller
    {
        [Route("About")]
        public IActionResult About()
        {
            return View();
        }
    }
}
