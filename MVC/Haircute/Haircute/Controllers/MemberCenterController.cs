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
            int ID = 1000;
            List<設計師服務品項> data = new 設計師資料().設計師服務品項(ID);
            return View(data);
        }
    }
}