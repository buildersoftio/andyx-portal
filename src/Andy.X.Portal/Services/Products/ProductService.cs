using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Products;
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

        public ProductListViewModel GetProductListViewModel()
        {
            var products = new ProductListViewModel();
            var tenants = tenantService.GetTenantListViewModel();

            foreach (var tenant in tenants.Tenants)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

                string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/tenants/{tenant}/products";
                client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

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

        public ProductDetailsViewModel GetProductDetailsViewModel(string tenant, string product)
        {
            var productDetails = new ProductDetailsViewModel();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v1/tenants/{tenant}/products/{product}";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                productDetails = JsonConvert.DeserializeObject<ProductDetailsViewModel>(content);
            }
            productDetails.Tenant = tenant;

            return productDetails;
        }

    }
}
