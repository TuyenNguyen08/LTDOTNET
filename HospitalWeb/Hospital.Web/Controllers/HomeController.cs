using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult TinTuc()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Blog_detail()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Blog()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Doctor()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Doctor_detail()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Hoidap()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Hoidap_detail()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult DichVu()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
