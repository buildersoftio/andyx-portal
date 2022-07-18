using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class ClustersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
