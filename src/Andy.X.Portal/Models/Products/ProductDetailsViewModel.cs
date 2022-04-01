using System;
using System.Collections.Concurrent;

namespace Andy.X.Portal.Models.Products
{
    public class ProductDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Tenant { get; set; }
        public string Name { get; set; }
        public ConcurrentDictionary<string, ComponentInProduct> Components { get; set; }
        public ProductDetailsViewModel()
        {
            Components = new ConcurrentDictionary<string, ComponentInProduct>();
        }
    }

    public class ComponentInProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
