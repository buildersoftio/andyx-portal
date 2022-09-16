using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Products;
using Andy.X.Portal.Models.Tenants;
using Andy.X.Portal.Services.Tenants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Andy.X.Portal.Services.Products
{
    public class ProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly TenantService tenantService;
        private readonly XNodeConfiguration xNodeConfiguration;

        public ProductService(ILogger<ProductService> logger, TenantService tenantService, XNodeConfiguration xNodeConfiguration)
        {
            this.logger = logger;
            this.tenantService = tenantService;
            this.xNodeConfiguration = xNodeConfiguration;
        }

        public ProductListViewModel GetProductListViewModel(Models.User user)
        {
            var products = new ProductListViewModel();
            var tenants = tenantService.GetTenantListViewModel(user);

            foreach (var tenant in tenants.Tenants)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/tenants/{tenant}/products";
                client.AddBasicAuthorizationHeader(user.Username, user.Password);

                HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var list = JsonConvert.DeserializeObject<List<Product>>(content);
                    list.ForEach(product =>
                    {
                        products.Products.Add(new ProductListDetail() { Tenant = tenant, Product = product.Name });
                    });
                }
            }

            return products;
        }

        public ProductDetailsViewModel GetProductDetailsViewModel(Models.User user, string tenant, string product)
        {
            var productDetails = new ProductDetailsViewModel();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                productDetails = JsonConvert.DeserializeObject<ProductDetailsViewModel>(content);
            }

            productDetails.Tenant = tenant;

            productDetails.Components = GetComponents(user, tenant, product);
            productDetails.ProductSettings = GetProductSettings(user, tenant, product);
            productDetails.Tokens = GetProductTokens(user, tenant, product);
            productDetails.ProductRetentions = GetProductRetentions(user, tenant, product);

            return productDetails;
        }

        private List<string> GetComponents(Models.User user, string tenant, string product)
        {
            var components = new List<string>();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/components";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                components = JsonConvert.DeserializeObject<List<string>>(content);
            }


            return components;
        }
        private ProductSettings GetProductSettings(Models.User user, string tenant, string product)
        {
            var result = new ProductSettings();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/settings";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<ProductSettings>(content);
            }


            return result;
        }
        private List<Token> GetProductTokens(Models.User user, string tenant, string product)
        {
            var result = new List<Token>();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/tokens";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<List<Token>>(content);
            }


            return result;
        }
        private List<ProductRetention> GetProductRetentions(Models.User user, string tenant, string product)
        {
            var result = new List<ProductRetention>();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenant}/products/{product}/retentions";
            client.AddBasicAuthorizationHeader(user.Username, user.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<List<ProductRetention>>(content);
            }


            return result;
        }

    }
}
