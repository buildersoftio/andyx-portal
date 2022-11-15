using Andy.X.Portal.Services.Topics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Andy.X.Portal.Controllers
{
    [Authorize]
    public class TopicsController : Controller
    {
        private readonly TopicService topicService;

        public TopicsController(TopicService topicService)
        {
            this.topicService = topicService;
        }

        public IActionResult Details(string tenant, string product, string component, string id)
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(topicService.GetTopicDetailsViewModel(user, tenant, product, component, id));
        }
    }
}
