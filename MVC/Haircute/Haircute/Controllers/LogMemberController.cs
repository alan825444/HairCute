using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;

namespace Haircute.Controllers
{
    [Authorize]
    public class LogMemberController : Controller
    {

        // GET: LogMember
        [Authorize]
        public ActionResult Index()
        {
            return View("../Home/Index", "_LayourMember");
        }
    }
}
