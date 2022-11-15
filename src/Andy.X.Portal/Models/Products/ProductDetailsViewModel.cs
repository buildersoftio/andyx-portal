using Andy.X.Portal.Models.Tenants;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Products
{
    public class ProductDetailsViewModel
    {
        public long Id { get; set; }
        public string Tenant { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public DateTimeOffset? UpdatedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }

        public List<string> Components{ get; set; }
        public ProductSettings ProductSettings { get; set; }
        public List<Token> Tokens { get; set; }
        public List<ProductRetention> ProductRetentions { get; set; }

        public ProductDetailsViewModel()
        {
            Components = new List<string>();
            ProductSettings = new ProductSettings();
            Tokens = new List<Token>();
            ProductRetentions = new List<ProductRetention>();
        }
    }

    public class ProductSettings
    {
        public bool IsAuthorizationEnabled { get; set; }
    }

    public class ProductRetention
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public RetentionType Type { get; set; }
        public long TimeToLiveInMinutes { get; set; }
    }

}
