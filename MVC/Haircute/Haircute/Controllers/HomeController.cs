using Haircute.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Haircute.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RGFDesigner()
        {
            ViewBag.DS = "T";
            return View();
        }

        [HttpPost]
        public ActionResult RGFDesigner(CMenberViewModel m)
        {
            demodbEntities db = new demodbEntities();
            db.tMember.Add(m.Member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}