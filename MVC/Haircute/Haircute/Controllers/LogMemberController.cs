using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using Haircute.Models;

namespace Haircute.Controllers
{
    [Authorize]
    public class LogMemberController : Controller
    {

        // GET: LogMember
        [Authorize]
        public ActionResult Index()
        {
            SelectList selectLists = new SelectList(new CityArea().getcity(), "fID", "fCity");
            ViewBag.SelectList = selectLists;
            ViewBag.圖片 = new 資料產生器().取得最新上傳();
            ViewBag.關鍵字 = new 資料產生器().取得關鍵字();
            return View("../Home/Index", "_LayourMember");
        }
    }
}
