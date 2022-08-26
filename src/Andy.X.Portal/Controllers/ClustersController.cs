using Andy.X.Portal.Services.Clusters;
using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class ClustersController : Controller
    {
        private readonly ClusterService clusterService;

        public ClustersController(ClusterService clusterService)
        {
            this.clusterService = clusterService;
        }

        public IActionResult Index()
        {
            return View(clusterService.GetClustersDetailsViewModel());
        }
    }
}
