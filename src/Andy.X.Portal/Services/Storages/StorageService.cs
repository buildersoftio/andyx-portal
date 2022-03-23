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

        public StorageService(ILogger<StorageService> logger)
        {
            this.logger = logger;
        }

        public StorageListViewModel GetStorageList()
        {
            StorageListViewModel storageListViewModel = new StorageListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"http://localhost:9001/api/v1/storages";

            // Move authorization as a seperate service
            client.AddBasicAuthorizationHeader("admin", "admin");

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

            string request = $"http://localhost:9001/api/v1/storages/{storageDetails}";
            client.AddBasicAuthorizationHeader("admin", "admin");

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
