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

        public ProductService(ILogger<ProductService> logger, TenantService tenantService)
        {
            this.logger = logger;
            this.tenantService = tenantService;
        }

        public ProductListViewModel GetProductListViewModel()
        {
            var products = new ProductListViewModel();
            var tenants = tenantService.GetTenantListViewModel();

            foreach (var tenant in tenants.Tenants)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-called-by", $"Andy X Portal");

                string request = $"http://localhost:9001/api/v1/tenants/{tenant}/products";
                client.AddBasicAuthorizationHeader("admin", "admin");

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
    }
}
