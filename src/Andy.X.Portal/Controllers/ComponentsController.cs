using Andy.X.Portal.Services.Components;
using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ComponentService componentService;

        public ComponentsController(ComponentService componentService)
        {
            this.componentService = componentService;
        }

        public IActionResult Details(string tenant, string product, string id)
        {
            return View(componentService.GetComponentDetailsViewModel(tenant, product, id));
        }
    }
}
