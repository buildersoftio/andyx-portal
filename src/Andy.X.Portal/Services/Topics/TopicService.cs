using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Topics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
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

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{component}/topics/{topic}";
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

            topicDetailsViewModel.TopicSettings = GetTopicSettings(tenant, product, component, topic);
            topicDetailsViewModel.Producers = GetTopicProducers(tenant, product, component, topic);
            topicDetailsViewModel.Subscriptions = GetSubscriptions(tenant, product, component, topic);

            return topicDetailsViewModel;
        }

        private TopicSettings GetTopicSettings(string tenant, string product, string component, string topic)
        {
            var result = new TopicSettings();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{component}/topics/{topic}/settings";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<TopicSettings>(content);
            }

            return result;
        }

        private List<string> GetTopicProducers(string tenant, string product, string component, string topic)
        {
            var results = new List<string>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{component}/topics/{topic}/producers";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                results = JsonConvert.DeserializeObject<List<string>>(content);
            }

            return results;
        }

        private List<TopicSubscription> GetSubscriptions(string tenant, string product, string component, string topic)
        {
            var results = new List<TopicSubscription>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{component}/topics/{topic}/subscriptions";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                results = JsonConvert.DeserializeObject<List<TopicSubscription>>(content);
            }

            return results;
        }



    }
}
