using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Tenants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Andy.X.Portal.Services.Tenants
{
    public class TenantService
    {
        private readonly ILogger<TenantService> logger;

        public TenantService(ILogger<TenantService> logger)
        {
            this.logger = logger;
        }

        public TenantListViewModel GetTenantListViewModel()
        {
            TenantListViewModel tenantListViewModel = new TenantListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"http://localhost:9001/api/v1/tenants";
            client.AddBasicAuthorizationHeader("admin", "admin");

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<List<string>>(content);
                tenantListViewModel.Tenants = list;
            }

            return tenantListViewModel;
        }

        public TenantDetailsViewModel GetTenantDetailsViewModel(string tenantName)
        {
            TenantDetailsViewModel tenantDetails = new TenantDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"http://localhost:9001/api/v1/tenants/{tenantName}";
            client.AddBasicAuthorizationHeader("admin", "admin");

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                tenantDetails = JsonConvert.DeserializeObject<TenantDetailsViewModel>(content);
            }

            return tenantDetails;
        }
    }
}
