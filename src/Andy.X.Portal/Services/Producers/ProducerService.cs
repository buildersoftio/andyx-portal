using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Producers;
using Andy.X.Portal.Models.Products;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;

namespace Andy.X.Portal.Services.Producers
{
    public class ProducerService
    {
        private readonly ILogger<ProducerService> logger;
        private readonly XNodeConfiguration xNodeConfiguration;

        public ProducerService(ILogger<ProducerService> logger, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public ProducerListViewModel GetProducerListViewModel(Models.User user)
        {
            ProducerListViewModel producerListViewModel = new ProducerListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/activities/producers";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<List<ProducerActivity>>(content);
                producerListViewModel.Producers = list;
            }

            foreach (var item in producerListViewModel.Producers)
            {
                var locations = item.Location.Split("/");
                item.Tenant = locations[0];
                item.Product = locations[1];
                item.Component = locations[2];
                item.Topic = locations[3];
            }

            return producerListViewModel;
        }

        public ProducerDetailsViewModel GetProducerDetailsViewModel(Models.User user, string tenant, string product, string component, string topic, string producerName)
        {
            var producerListViewModel = new ProducerDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");
            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{component}/topics/{topic}/producers/{producerName}";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                producerListViewModel = JsonConvert.DeserializeObject<ProducerDetailsViewModel>(content);
            }

            producerListViewModel.Tenant = tenant;
            producerListViewModel.Product = product;
            producerListViewModel.Component = component;
            producerListViewModel.Topic = topic;

            return producerListViewModel;
        }
    }
}
