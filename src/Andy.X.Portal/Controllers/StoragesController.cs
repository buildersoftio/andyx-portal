using Andy.X.Portal.Services.Storages;
using Microsoft.AspNetCore.Mvc;

namespace Andy.X.Portal.Controllers
{
    public class StoragesController : Controller
    {
        private readonly StorageService storageService;

        public StoragesController(StorageService storageService)
        {
            this.storageService = storageService;
        }


        public IActionResult Index()
        {
            return View(storageService.GetStorageList());
        }

        public IActionResult Details(string id)
        {
            return View(storageService.GetStorageDetails(id));
        }
    }
}
