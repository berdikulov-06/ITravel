using Microsoft.AspNetCore.Mvc;

namespace ITravel.Controllers
{
    public class ContactController : Controller
    {
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
