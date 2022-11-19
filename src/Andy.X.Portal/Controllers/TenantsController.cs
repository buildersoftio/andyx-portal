using Andy.X.Portal.Services.Tenants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Andy.X.Portal.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly TenantService tenantService;

        public TenantsController(TenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        public IActionResult Index()
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(tenantService.GetTenantListViewModel(user));
        }

        public IActionResult Details(string id)
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(tenantService.GetTenantDetailsViewModel(user, id));
        }
    }
}
