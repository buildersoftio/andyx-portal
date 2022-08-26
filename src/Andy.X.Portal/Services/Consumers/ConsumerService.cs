using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Consumers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Andy.X.Portal.Services.Consumers
{
    public class ConsumerService
    {
        private readonly ILogger<ConsumerService> logger;
        private readonly XNodeConfiguration xNodeConfiguration;

        public ConsumerService(ILogger<ConsumerService> logger, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public ConsumerListViewModel GetConsumerListViewModel()
        {
            ConsumerListViewModel consumerListViewModel = new ConsumerListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/consumers";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<List<string>>(content);
                consumerListViewModel.Consumers = list;
            }


            return consumerListViewModel;
        }

        public ConsumerDetailsViewModel GetConsumerDetailsViewModel(string consumerName)
        {
            var consumerDetailsViewModel = new ConsumerDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/consumers/{consumerName}";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                consumerDetailsViewModel = JsonConvert.DeserializeObject<ConsumerDetailsViewModel>(content);
            }

            return consumerDetailsViewModel;
        }
    }
}
