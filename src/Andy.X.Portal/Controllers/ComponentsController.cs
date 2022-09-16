using Andy.X.Portal.Services.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Andy.X.Portal.Controllers
{
    [Authorize]
    public class ComponentsController : Controller
    {
        private readonly ComponentService componentService;

        public ComponentsController(ComponentService componentService)
        {
            this.componentService = componentService;
        }

        public IActionResult Details(string tenant, string product, string id)
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(componentService.GetComponentDetailsViewModel(user, tenant, product, id));
        }
    }
}
