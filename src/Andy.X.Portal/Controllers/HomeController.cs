using Andy.X.Portal.Models;
using Andy.X.Portal.Models.Home;
using Andy.X.Portal.Services.Home;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Andy.X.Portal.Services.User;

namespace Andy.X.Portal.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger, HomeService homeService, UserService userService)
        {
            _logger = logger;
            _homeService = homeService;
            _userService = userService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var user = new Models.User()
            {
                Password = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Password").Value,
                Username = HttpContext.User.Identity.Name
            };
            return View(_homeService.GetHomeViewModel(user));
        }

        [Authorize]
        public IActionResult License()
        {
            return View();
        }

        public IActionResult Login(string ReturnUrl = "/")
        {
            User objLoginModel = new User();
            objLoginModel.ReturnUrl = ReturnUrl;
            return View(objLoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(User objLoginModel)
        {
            if (ModelState.IsValid)
            {
                string user = objLoginModel.Username;
                string password = objLoginModel.Password;
                string role = "";
                if (_userService.TryGetUserRole(user, password, out role) != true)
                {
                    //Add logic here to display some message to user    
                    ViewBag.Message = "Invalid Credential";
                    return View(objLoginModel);
                }
                else
                {
                    //A claim is a statement about a subject by an issuer and    
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user)),
                        new Claim(ClaimTypes.Name, user),
                        new Claim(ClaimTypes.Role, role),
                        new Claim("Password", password)
                };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = objLoginModel.RememberLogin
                    });
                    return LocalRedirect(objLoginModel.ReturnUrl);
                }
            }
            return View(objLoginModel);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
