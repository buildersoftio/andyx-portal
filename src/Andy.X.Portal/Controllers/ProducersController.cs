using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class ProducersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
