using Andy.X.Portal.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Andy.X.Portal.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ProductService productService;

        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(productService.GetProductListViewModel(user));
        }

        public IActionResult Details(string tenant, string id)
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(productService.GetProductDetailsViewModel(user, tenant, id));
        }
    }
}
