using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Components;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace Andy.X.Portal.Services.Components
{
    public class ComponentService
    {
        private readonly ILogger<ComponentService> logger;
        private readonly XNodeConfiguration xNodeConfiguration;

        public ComponentService(ILogger<ComponentService> logger, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.xNodeConfiguration = xNodeConfiguration;
        }


        public ComponentDetailsViewModel GetComponentDetailsViewModel(string tenant, string product, string componentName)
        {
            var componentDetailsViewModel = new ComponentDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/tenants/{tenant}/products/{product}/components/{componentName}";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                componentDetailsViewModel = JsonConvert.DeserializeObject<ComponentDetailsViewModel>(content);
            }
            componentDetailsViewModel.Tenant = tenant;
            componentDetailsViewModel.Product = product;

            return componentDetailsViewModel;
        }
    }
}
