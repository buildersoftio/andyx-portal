using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Subscriptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Andy.X.Portal.Services.Subscriptions
{
    public class SubscriptionService
    {
        private readonly ILogger<SubscriptionService> logger;
        private readonly XNodeConfiguration xNodeConfiguration;

        public SubscriptionService(ILogger<SubscriptionService> logger, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public SubscriptionListViewModel GetOnlineSubscriptionsListViewModel(Models.User user)
        {
            SubscriptionListViewModel consumerListViewModel = new SubscriptionListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/activities/subscriptions";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<List<SubscriptionActivity>>(content);
                consumerListViewModel.Subscriptions = list;
            }

            foreach (var item in consumerListViewModel.Subscriptions)
            {
                var locations = item.Location.Split("/");
                item.Tenant = locations[0];
                item.Product = locations[1];
                item.Component = locations[2];
                item.Topic = locations[3];
            }

            return consumerListViewModel;
        }

        public SubscriptionDetailsViewModel GetSubscriptionDetailsViewModel(Models.User user, string tenant, string product, string component, string topic, string subscription)
        {
            var consumerDetailsViewModel = new SubscriptionDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{component}/topics/{topic}/subscriptions/{subscription}";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                consumerDetailsViewModel = JsonConvert.DeserializeObject<SubscriptionDetailsViewModel>(content);
            }

            consumerDetailsViewModel.Tenant = tenant;
            consumerDetailsViewModel.Product = product;
            consumerDetailsViewModel.Component = component;
            consumerDetailsViewModel.Topic = topic;

            return consumerDetailsViewModel;
        }
    }
}
