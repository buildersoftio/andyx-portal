using Andy.X.Portal.Services.Consumers;
using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class ConsumersController : Controller
    {
        private readonly ConsumerService consumerService;

        public ConsumersController(ConsumerService consumerService)
        {
            this.consumerService = consumerService;
        }

        public IActionResult Index()
        {
            return View(consumerService.GetConsumerListViewModel());
        }
    }
}
