using System;
using System.Collections.Generic;

namespace Andy.X.Portal.Models.Products
{
    public class ProductListViewModel
    {
        public List<ProductListDetail> Products { get; set; }

        public ProductListViewModel()
        {
            Products = new List<ProductListDetail>();
        }
    }
    public class ProductListDetail
    {
        public string Tenant { get; set; }
        public string Product { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
