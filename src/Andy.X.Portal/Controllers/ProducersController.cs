using Andy.X.Portal.Services.Producers;
using Microsoft.AspNetCore.Mvc;

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
            return View(producerService.GetProducerListViewModel());
        }

        public IActionResult Details(string id)
        {
            return View(producerService.GetProducerDetailsViewModel(id));
        }
    }
}
