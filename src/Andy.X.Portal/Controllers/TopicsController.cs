using Andy.X.Portal.Services.Topics;
using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class TopicsController : Controller
    {
        private readonly TopicService topicService;

        public TopicsController(TopicService topicService)
        {
            this.topicService = topicService;
        }

        public IActionResult Details(string tenant, string product, string component, string id)
        {
            return View(topicService.GetTopicDetailsViewModel(tenant, product, component, id));
        }
    }
}
