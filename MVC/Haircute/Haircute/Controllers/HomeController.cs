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
            if (User.Identity.Name != "")
            {
                SelectList selectLists = new SelectList(new CityArea().getcity(), "fID", "fCity");
                ViewBag.SelectList = selectLists;
                ViewBag.圖片 = new 資料產生器().取得最新上傳();
                ViewBag.關鍵字 = new 資料產生器().取得關鍵字();
                ViewBag.評論 = new 資料產生器().評論回傳();
                ViewBag.預約排行 = new 資料產生器().預約排行();

                return View("", "_LayourMember");
            }
            else
            {
                SelectList selectLists = new SelectList(new CityArea().getcity(), "fID", "fCity");
                ViewBag.SelectList = selectLists;
                ViewBag.圖片 = new 資料產生器().取得最新上傳();
                ViewBag.關鍵字 = new 資料產生器().取得關鍵字();
                ViewBag.評論 = new 資料產生器().評論回傳();
                ViewBag.預約排行 = new 資料產生器().預約排行();
                return View();
            }
            
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
            SelectList selectLists = new SelectList(new CityArea().getcity(), "fID", "fCity");
            ViewBag.SelectList = selectLists;
            demodbEntities db = new demodbEntities();
            var member = db.tMember.Where(k => k.fEmail == m.fEmail).FirstOrDefault();
            if ((member == null)&&(m.fCity!=null)&&(m.fArea!=null))
            {
                try
                {
                    db.tMember.Add(m.Member);
                    db.SaveChanges();
                    var id = db.tMember.Where(k => k.fEmail == m.fEmail).FirstOrDefault().fID;
                    //屬於mail功能部份
                    //db.tDesigner.Add(new tDesigner { fk_Member = id });
                    //db.SaveChanges();
                    //db.tWork.Add(new tWork { fk_Designer = db.tDesigner.Where(x=>x.fk_Member == id).FirstOrDefault().fid });
                    //db.SaveChanges();
                    new registFunction().SendEmail(m.fID.ToString(), m.fEmail).Wait();
                    return RedirectToAction("ConfirmPage");
                }
                catch (Exception exe)
                {

                    throw;
                }
                
            }
            else if ((member == null) && ((m.fCity == null) || (m.fArea == null)))
            {
                ViewBag.Message1 = "請選擇居住區域";
            }
            else if ((member != null) && ((m.fCity == null) || (m.fArea == null)))
            {
                ViewBag.Message1 = "請選擇居住區域";
                ViewBag.Message = "此帳號已有人使用";
            }
            ViewBag.Message = "此帳號已有人使用";

            return View();
        }

        public ActionResult UserRG() 
        {
            ViewBag.DS = "F";
            SelectList selectLists = new SelectList(new CityArea().getcity(), "fID", "fCity");
            ViewBag.SelectList = selectLists;
            return View();
        }

        [HttpPost]
        public ActionResult UserRG(CMenberViewModel m)
        {
            SelectList selectLists = new SelectList(new CityArea().getcity(), "fID", "fCity");
            ViewBag.SelectList = selectLists;
            demodbEntities db = new demodbEntities();
            var member = db.tMember.Where(k => k.fEmail == m.fEmail).FirstOrDefault();
            if ((member == null) && (m.fCity != null) && (m.fArea != null))
            {
                db.tMember.Add(m.Member);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else if ((member == null) && ((m.fCity == null) || (m.fArea == null)))
            {
                ViewBag.Message1 = "請選擇居住區域";
            }
            else if ((member != null) && ((m.fCity == null) || (m.fArea == null)))
            {
                ViewBag.Message1 = "請選擇居住區域";
                ViewBag.Message = "此帳號已有人使用";
            }
            ViewBag.Message = "此帳號已有人使用";
            return View();
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
            if (認證.fconfirmation == null)
            {
                認證.fconfirmation = "Y";
                db.SaveChanges();
                db.tDesigner.Add(new tDesigner { fk_Member = cfID });
                db.SaveChanges();
                db.tWork.Add(new tWork { fk_Designer = db.tDesigner.Where(x => x.fk_Member == cfID).FirstOrDefault().fid });
                db.SaveChanges();
                FormsAuthentication.RedirectFromLoginPage(認證.fID.ToString(), true);
                Session["Member"] = 認證.fNickname;
                Session["ID"] = 認證.fID.ToString();
                TempData["result"] = "認證成功，請到會員中心編輯店鋪資訊";
                return RedirectToAction("ConfirmedPage");
            }
            else
            {
                Session["Member"] = 認證.fNickname;
                Session["ID"] = 認證.fID.ToString();
                TempData["result"] = "您的帳號已通過驗證";
                return RedirectToAction("ConfirmedPage");
            }
           
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string txtAcc, string txtPwd)
        {
            demodbEntities db = new demodbEntities();
            var member = db.tMember.Where(m => m.fEmail == txtAcc && m.fPwd == txtPwd).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "帳號密碼錯誤";
                return View("Login");
            }
            FormsAuthentication.RedirectFromLoginPage(member.fID.ToString(),true);
            Session["Member"] = member.fNickname;
            Session["ID"] = member.fID.ToString();
            return RedirectToAction("Index", "LogMember");
        }

        public ActionResult ConfirmPage()
        {
            return View();
        }

        public ActionResult ConfirmedPage()
        {
            if (TempData["result"] != null)
            {
                ViewBag.result = TempData["result"];
            }
            else
            {
                ViewBag.result = "您的帳號已通過驗證";
            }

            return View();
        }
        public ActionResult Search() 
        {
            string Strtest = TempData["test"].ToString();
            string StrCity = TempData["City"].ToString();
            string StrArea = TempData["Area"].ToString();
            return View();
        }
        [HttpPost]
        public ActionResult Search(string Keyword, string fCity, string fArea) 
        {
            TempData["test"] = Keyword;
            TempData["City"] = fCity;
            TempData["Area"] = fArea;
            return RedirectToAction("Search", "Search");
        }

        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult ForgotPassword() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            var result = new registFunction().忘記密碼(Email);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult PwdReset(string ID) 
        {
            ViewBag.ID = Convert.ToInt32(new registFunction().decryptstr(ID));
            return View();
        }

        public ActionResult PwdRes(string id, string Pwd, string ResetPwd)
        {

            var result = new registFunction().重設密碼功能(id, Pwd, ResetPwd);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

}