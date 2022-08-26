using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Topics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace Andy.X.Portal.Services.Topics
{
    public class TopicService
    {
        private readonly ILogger<TopicService> logger;
        private readonly XNodeConfiguration xNodeConfiguration;

        public TopicService(ILogger<TopicService> logger, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public TopicDetailsViewModel GetTopicDetailsViewModel(string tenant, string product, string component, string topic)
        {
            var topicDetailsViewModel = new TopicDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/tenants/{tenant}/products/{product}/components/{component}/topics/{topic}";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                topicDetailsViewModel = JsonConvert.DeserializeObject<TopicDetailsViewModel>(content);
            }
            topicDetailsViewModel.Tenant = tenant;
            topicDetailsViewModel.Product = product;
            topicDetailsViewModel.Component = component;

            return topicDetailsViewModel;
        }
    }
}
