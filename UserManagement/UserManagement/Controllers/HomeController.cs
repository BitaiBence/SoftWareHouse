using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.User objUser)
        {
            CsvDb db = new CsvDb("uj.txt");
            var users= db.GetAllAsync().Result;

            if (users.Any(u => u.Name == objUser.Name))
            {
                return View("Grid");
            }
            return View("Index");
            //return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View("Grid");
        }

        public IActionResult Grid()
        {
            return View("Grid");
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

        public IActionResult Error()
        {
            return View();
        }

        // GET: DetailsController/Details/5
        public ActionResult Details(int id)
        {
            return View("DetailsView");
        }
    }
}
