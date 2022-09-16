using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Components;
using Andy.X.Portal.Models.Lineage;
using Andy.X.Portal.Models.Tenants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        public ComponentDetailsViewModel GetComponentDetailsViewModel(Models.User user, string tenant, string product, string componentName)
        {
            var componentDetailsViewModel = new ComponentDetailsViewModel();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{componentName}";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                componentDetailsViewModel = JsonConvert.DeserializeObject<ComponentDetailsViewModel>(content);
            }

            componentDetailsViewModel.Tenant = tenant;
            componentDetailsViewModel.Product = product;

            componentDetailsViewModel.ComponentSettings = GetComponentSettings(user,tenant, product, componentName);
            componentDetailsViewModel.Topics = GetTopics(user, tenant, product, componentName);
            componentDetailsViewModel.Tokens = GetTokens(user, tenant, product, componentName);
            componentDetailsViewModel.ComponentRetentions = GetRetentions(user, tenant, product, componentName);

            return componentDetailsViewModel;
        }

        private ComponentSettings GetComponentSettings(Models.User user, string tenant, string product, string componentName)
        {
            var result = new ComponentSettings();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{componentName}/settings";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<ComponentSettings>(content);
            }

            return result;
        }
        private List<string> GetTopics(Models.User user, string tenant, string product, string componentName)
        {
            var result = new List<string>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{componentName}/topics";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<List<string>>(content);
            }

            return result;
        }
        private List<Token> GetTokens(Models.User user, string tenant, string product, string componentName)
        {
            var result = new List<Token>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{componentName}/tokens";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<List<Token>>(content);
            }

            return result;
        }
        private List<ComponentRetention> GetRetentions(Models.User user, string tenant, string product, string componentName)
        {
            var result = new List<ComponentRetention>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components/{componentName}/retentions";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<List<ComponentRetention>>(content);
            }

            return result;
        }
    }
}
