using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Haircute.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string test , string fCity, string fArea) 
        {
            TempData["test"] = test;
            TempData["City"] = fCity;
            TempData["Area"] = fArea;
            return RedirectToAction("Index","Search");
        }
    }
}