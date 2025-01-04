using Microsoft.AspNetCore.Mvc;

namespace Car.Controllers
{
    public class RecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
