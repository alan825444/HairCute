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
            var q = "";
            var q2 = 0;
            var q3 = 0;

            if ((TempData["City"] !=null) || (TempData["Area"] !=null) || (TempData["Area"]!=null))
            {
                if ((TempData["City"].ToString() == "") && (TempData["Area"].ToString() == "--------") && (TempData["Area"].ToString() == ""))
                {
                    ViewBag.搜索結果 = null;
                }
                else if (((TempData["City"].ToString() == "") || (TempData["Area"].ToString() == "--------")) && (TempData["Area"].ToString() != ""))
                {
                    q = TempData["test"].ToString();
                    q2 = 0;
                    q3 = 0;
                    ViewBag.搜索結果 = new 資料產生器().回傳搜尋結果(q2, q3, q);
                }
                else
                {
                    q = TempData["test"].ToString();
                    q2 = Convert.ToInt32(TempData["City"]);
                    q3 = Convert.ToInt32(TempData["Area"]);
                    ViewBag.搜索結果 = new 資料產生器().回傳搜尋結果(q2, q3, q);
                }
            }
            else
            {
                ViewBag.搜索結果 = null;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Search(List<string> test , string fCity, string fArea) 
        {
            TempData["test"] = "";
            if (test != null)
            {
                if (test.Count > 1)
                {
                    foreach (var item in test)
                    {
                        TempData["test"] += item + "/";
                    }
                }
                foreach (var item in test)
                {
                    TempData["test"] += item;
                }
            }

            TempData["City"] = fCity;
            TempData["Area"] = fArea;
            return RedirectToAction("Index","Search");
        }
    }
}