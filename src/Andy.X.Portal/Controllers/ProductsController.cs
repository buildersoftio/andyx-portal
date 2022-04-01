using Andy.X.Portal.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService productService;

        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View(productService.GetProductListViewModel());
        }

        public IActionResult Details(string tenant, string id)
        {
            return View(productService.GetProductDetailsViewModel(tenant, id));
        }
    }
}
