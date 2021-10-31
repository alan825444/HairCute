using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haircute.Models;

namespace Haircute.Controllers
{
    public class MemberCenterController : Controller
    {
        // GET: MemberCenter
        
        public ActionResult Index()
        {
            var A = 資料庫.查詢<tMember>(1000);
            var B = 資料庫.查詢<tDesigner>(A.fk_Designer).tService;
            ViewBag.C = B;
            return View();
        }
    }
}