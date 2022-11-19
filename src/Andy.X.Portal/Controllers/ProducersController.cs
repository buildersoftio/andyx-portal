using Andy.X.Portal.Services.Producers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Andy.X.Portal.Controllers
{
    public class ProducersController : Controller
    {
        private readonly ProducerService producerService;

        public ProducersController(ProducerService producerService)
        {
            this.producerService = producerService;
        }

        public IActionResult Index()
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(producerService.GetProducerListViewModel(user));
        }

        public IActionResult Details(string tenant, string product, string component, string topic, string id)
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(producerService.GetProducerDetailsViewModel(user, tenant, product, component, topic, id));
        }
    }
}
