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

        public ProducerService(ILogger<ProducerService> logger)
        {
            this.logger = logger;
        }

        public ProducerListViewModel GetProducerListViewModel()
        {
            ProducerListViewModel producerListViewModel = new ProducerListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"http://localhost:9001/api/v1/producers";
            client.AddBasicAuthorizationHeader("admin", "admin");

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<List<string>>(content);
                producerListViewModel.Producers = list;
            }


            return producerListViewModel;
        }
    }
}
