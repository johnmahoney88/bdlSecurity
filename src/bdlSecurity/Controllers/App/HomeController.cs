using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using bdlSecurity.ViewModels;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;
using bdlSecurity.Models;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http.Features;

namespace bdlSecurity.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public IActionResult Index()
        {
            // land on request index first
            return RedirectToAction("Index", "Request");

            //ViewBag.Title = "BDL Security Application";
            //return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Help()
        {
            ViewBag.Message = "Help for Badge system";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error(String id)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = feature?.Error;
            var iisError = id;

            return View(exception);
        }
    }
}
