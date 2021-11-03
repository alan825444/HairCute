using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Haircute.Controllers
{
    public class DesgnerPageController : Controller
    {
        // GET: DesgnerPage
        public ActionResult DesignerPage()
        {
            if (Session["ID"] == null)
            {
                return View();
            }
            return View("", "_LayourMember");
        }
    }
}