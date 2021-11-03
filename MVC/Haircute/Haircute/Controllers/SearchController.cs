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
            if (Session["ID"] == null)
            {
                return View();
            }
            return View("", "_LayourMember");
        }
    }
}