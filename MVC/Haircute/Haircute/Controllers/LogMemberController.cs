using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Haircute.Controllers
{
    public class LogMemberController : Controller
    {

        // GET: LogMember
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}