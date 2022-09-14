﻿using Andy.X.Portal.Configurations;
using Andy.X.Portal.Extensions;
using Andy.X.Portal.Models.Tenants;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
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

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants";
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

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenantName}";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                tenantDetails = JsonConvert.DeserializeObject<TenantDetailsViewModel>(content);
            }

            tenantDetails.Settings = GetTenantSettings(tenantName);
            tenantDetails.Products = GetTenantProducts(tenantName);
            tenantDetails.Tokens = GetTenantTokens(tenantName);
            tenantDetails.TenantRetentions = GetTenantRetentions(tenantName);

            return tenantDetails;
        }


        private TenantSettings GetTenantSettings(string tenantName)
        {
            var tenantSettings = new TenantSettings();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenantName}/settings";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                tenantSettings = JsonConvert.DeserializeObject<TenantSettings>(content);
            }

            return tenantSettings;
        }
        private List<string> GetTenantProducts(string tenantName)
        {
            var productList = new List<string>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenantName}/products";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                productList = JsonConvert.DeserializeObject<List<string>>(content);
            }

            return productList;
        }
        private List<Token> GetTenantTokens(string tenantName)
        {
            var tenantTokens = new List<Token>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenantName}/tokens";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                tenantTokens = JsonConvert.DeserializeObject<List<Token>>(content);
            }

            return tenantTokens;
        }
        private List<TenantRetention> GetTenantRetentions(string tenantName)
        {
            var tenantRetentions = new List<TenantRetention>();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-called-by", $"andyx-portal v3");

            string request = $"{xNodeConfiguration.ServiceUrl}/api/v3/tenants/{tenantName}/retentions";
            client.AddBasicAuthorizationHeader(xNodeConfiguration.Username, xNodeConfiguration.Password);

            HttpResponseMessage httpResponseMessage = client.GetAsync(request).Result;
            string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                tenantRetentions = JsonConvert.DeserializeObject<List<TenantRetention>>(content);
            }

            return tenantRetentions;
        }
    }
}
