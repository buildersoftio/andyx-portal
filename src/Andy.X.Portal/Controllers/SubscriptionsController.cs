using Andy.X.Portal.Services.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Andy.X.Portal.Controllers
{
    [Authorize]
    public class SubscriptionsController : Controller
    {
        private readonly SubscriptionService consumerService;

        public SubscriptionsController(SubscriptionService consumerService)
        {
            this.consumerService = consumerService;
        }

        public IActionResult Index()
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(consumerService.GetOnlineSubscriptionsListViewModel(user));
        }

        public IActionResult Details(string tenant, string product, string component, string topic, string id)
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(consumerService.GetSubscriptionDetailsViewModel(user, tenant, product, component, topic, id));
        }
    }
}
