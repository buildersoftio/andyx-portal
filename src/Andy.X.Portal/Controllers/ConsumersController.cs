using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class ConsumersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
