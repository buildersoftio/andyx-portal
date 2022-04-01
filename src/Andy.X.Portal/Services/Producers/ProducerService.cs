using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Producers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        public ProducerListViewModel GetProducerListViewModel()
        {
            ProducerListViewModel producerListViewModel = new ProducerListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/producers";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<List<string>>(content);
                producerListViewModel.Producers = list;
            }

            return producerListViewModel;
        }

        public ProducerDetailsViewModel GetProducerDetailsViewModel(string producerName)
        {
            var producerListViewModel = new ProducerDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/producers/{producerName}";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                producerListViewModel = JsonConvert.DeserializeObject<ProducerDetailsViewModel>(content);
            }

            return producerListViewModel;
        }
    }
}
