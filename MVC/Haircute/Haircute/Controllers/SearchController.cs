using Haircute.Models;
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
            SelectList selectLists = new SelectList(new CityArea().getcity(), "fID", "fCity");
            ViewBag.SelectList = selectLists;
            ViewBag.關鍵字 = new 資料產生器().取得關鍵字();
            var q = TempData["test"].ToString();
            var q2 = 0;
            var q3 = 0;
            if ((TempData["City"].ToString() == "")||(TempData["Area"].ToString() == "--------")|| (TempData["Area"].ToString() == ""))
            {
                ViewBag.搜索結果 = null;
            }
            else
            {
                q2 = Convert.ToInt32(TempData["City"]);
                q3 = Convert.ToInt32(TempData["Area"]);
                ViewBag.搜索結果 = new 資料產生器().回傳搜尋結果(q2, q3, q);
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Search(List<string> test , string fCity, string fArea) 
        {
            TempData["test"] = "";
            if (test != null)
            {
                foreach (var item in test)
                {
                    TempData["test"] += item + "/";
                }
            }

            TempData["City"] = fCity;
            TempData["Area"] = fArea;
            return RedirectToAction("Index","Search");
        }
    }
}