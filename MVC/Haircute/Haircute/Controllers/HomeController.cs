using Haircute.Models;
using Haircute.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


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
            SelectList selectLists = new SelectList(new CityArea().getcity(),"fID","fCity");
            ViewBag.SelectList = selectLists;
            return View();
        }

        [HttpPost]
        public ActionResult RGFDesigner(CMenberViewModel m)
        {
            demodbEntities db = new demodbEntities();
            db.tMember.Add(m.Member);
            db.SaveChanges();
            new registFunction().SendEmail(m.fID.ToString(), m.fEmail).Wait();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Area (string Cityid)
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            int ID = Convert.ToInt32(Cityid);
            if (!string.IsNullOrEmpty(Cityid))
            {
                var Area = new CityArea().GetArea(ID);
                if (Area.Count()>0)
                {
                    foreach (var A in Area)
                    {
                        items.Add(new KeyValuePair<string, string>(
                            A.fid.ToString(),
                            string.Format("{0}", A.fArea)));
                    }
                }

            }
            
            return this.Json(items);
        }

        public ActionResult mailtest(string ID) 
        {
            int cfID = Convert.ToInt32(new registFunction().decryptstr(ID));
            demodbEntities db = new demodbEntities();
            tMember 認證 = db.tMember.FirstOrDefault(p => p.fID == cfID);
            認證.fconfirmation = "Y";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string txtAcc, string txtPwd)
        {
            demodbEntities db = new demodbEntities();

            var q = db.tMember.Where(m => m.fEmail == txtAcc && m.fPwd == txtPwd).FirstOrDefault();
            if (q == null)
            {
                ViewBag.message = "請確認輸入的帳號密碼正確性";
                return RedirectToAction("Login");
            }
            FormsAuthentication.RedirectFromLoginPage(q.fID.ToString(),true);
            Session["Member"] = q.fNickname;
            return RedirectToAction("Index");
        }
    }
}