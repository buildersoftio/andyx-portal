using Andy.X.Portal.Configurations;
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
        private readonly XNodeConfiguration xNodeConfiguration;

        public TenantService(ILogger<TenantService> logger, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public TenantListViewModel GetTenantListViewModel()
        {
            TenantListViewModel tenantListViewModel = new TenantListViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/tenants";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

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
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/tenants/{tenantName}";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

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
