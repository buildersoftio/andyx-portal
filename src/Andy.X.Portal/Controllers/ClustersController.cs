using Andy.X.Portal.Services.Clusters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Andy.X.Portal.Controllers
{
    [Authorize]
    public class ClustersController : Controller
    {
        private readonly ClusterService clusterService;

        public ClustersController(ClusterService clusterService)
        {
            this.clusterService = clusterService;
        }

        public IActionResult Index()
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(clusterService.GetClustersDetailsViewModel(user));
        }
    }
}
