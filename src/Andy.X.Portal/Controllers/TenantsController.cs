using Andy.X.Portal.Services.Tenants;
using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class TenantsController : Controller
    {
        private readonly TenantService tenantService;

        public TenantsController(TenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        public IActionResult Index()
        {
            return View(tenantService.GetTenantListViewModel());
        }

        public IActionResult Details(string id)
        {
            return View(tenantService.GetTenantDetailsViewModel(id));
        }
    }
}
