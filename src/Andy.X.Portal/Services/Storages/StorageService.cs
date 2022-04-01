using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Storages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Andy.X.Portal.Services.Storages
{
    public class StorageService
    {
        private readonly ILogger<StorageService> logger;
        private readonly XNodeConfiguration xNodeConfiguration;

        public StorageService(ILogger<StorageService> logger, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public StorageListViewModel GetStorageList()
        {
            StorageListViewModel storageListViewModel = new StorageListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/storages";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<List<string>>(content);
                storageListViewModel.StorageNames = list;
            }

            return storageListViewModel;
        }

        public StorageDetailsViewModel GetStorageDetails(string storageDetails)
        {
            StorageDetailsViewModel storageDetailsViewModel = new StorageDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/storages/{storageDetails}";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                storageDetailsViewModel = JsonConvert.DeserializeObject<StorageDetailsViewModel>(content);
            }

            return storageDetailsViewModel;
        }
    }
}
