using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Home;
using Andy.X.Portal.Models.Subscriptions;
using Andy.X.Portal.Services.Products;
using Andy.X.Portal.Services.Tenants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Andy.X.Portal.Services.Home
{
    public class HomeService
    {
        private readonly ILogger<HomeService> logger;
        private readonly TenantService tenantService;
        private readonly XNodeConfiguration xNodeConfiguration;
        public HomeService(ILogger<HomeService> logger, TenantService tenantService, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.tenantService = tenantService;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public HomeViewModel GetHomeViewModel(Models.User user)
        {
            var results = new HomeViewModel();

            results.NodeUrl = xNodeConfiguration.ServiceUrl;
            results.TenantCount = GetTenantCount(user);
            results.ProductCount = GetProductCount(user);
            results.ComponentCount = GetComponentCount(user);
            results.TopicCount = GetTopicCount(user);

            results.ProducersCount = GetProducerCount(user);
            results.SubscriptionCount = GetSubscriptionCount(user);

            return results;
        }

        private long GetTenantCount(Models.User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/activities/tenants/count";
                client.AddBasicAuthorizationHeader(user.Username, user.Password);

                HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<long>(content);

                    return result;
                }
            }
            catch (Exception)
            {

            }
            return -1;
        }
        private long GetProductCount(Models.User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/activities/products/count";
                client.AddBasicAuthorizationHeader(user.Username, user.Password);

                HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<long>(content);

                    return result;
                }
            }
            catch (Exception)
            {

            }
            return -1;
        }
        private long GetComponentCount(Models.User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/activities/components/count";
                client.AddBasicAuthorizationHeader(user.Username, user.Password);

                HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<long>(content);

                    return result;
                }
            }
            catch (Exception)
            {

            }
            return -1;
        }
        private long GetTopicCount(Models.User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/activities/topics/count";
                client.AddBasicAuthorizationHeader(user.Username, user.Password);

                HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<long>(content);

                    return result;
                }
            }
            catch (Exception)
            {

            }
            return -1;
        }
        private long GetProducerCount(Models.User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/activities/producers/count";
                client.AddBasicAuthorizationHeader(user.Username, user.Password);

                HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<long>(content);

                    return result;
                }
            }
            catch (Exception)
            {

            }
            return -1;
        }
        private long GetSubscriptionCount(Models.User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/activities/subscriptions/count";
                client.AddBasicAuthorizationHeader(user.Username, user.Password);

                HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<long>(content);

                    return result;
                }
            }
            catch (Exception)
            {

            }
            return -1;
        }

    }
}
