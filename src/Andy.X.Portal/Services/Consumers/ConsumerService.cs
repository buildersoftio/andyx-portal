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

        public ConsumerService(ILogger<ConsumerService> logger)
        {
            this.logger = logger;
        }

        public ConsumerListViewModel GetConsumerListViewModel()
        {
            ConsumerListViewModel consumerListViewModel = new ConsumerListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"http://localhost:9001/api/v1/consumers";
            client.AddBasicAuthorizationHeader("admin", "admin");

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<List<string>>(content);
                consumerListViewModel.Consumers = list;
            }


            return consumerListViewModel;
        }
    }
}
