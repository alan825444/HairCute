using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Haircute.Controllers
{
    public class UserCenterController : Controller
    {
        // GET: UserCenter
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}