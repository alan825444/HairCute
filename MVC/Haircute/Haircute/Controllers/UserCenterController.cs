using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haircute.Models;

namespace Haircute.Controllers
{
    public class UserCenterController : Controller
    {
        // GET: UserCenter
        demodbEntities db = new demodbEntities();
        //[Authorize]
        public ActionResult Index()
        {
            int SSID = Convert.ToInt32(User.Identity.Name);
            var q = db.tMember.Where(x => x.fID == SSID).FirstOrDefault();
            if (q.fDesign == "T         ")
            {
                return RedirectToAction("Index", "MemberCenter");
            }
            else
            {
                ViewBag.Name = q.fNickname;
                //ViewBag.selectCity = new 資料產生器().selectedCity(SSID);
                //ViewBag.selectArea = new 資料產生器().selectedArea(SSID);
                ViewBag.selectCity2 = new 資料產生器().取得會員City(SSID);
                ViewBag.selectArea2 = new 資料產生器().取得會員Area(SSID);
                ViewBag.會員資料 = new 資料產生器().取得會員資料(SSID);
                ViewBag.預約紀錄 = new 資料產生器Test().預約(SSID);
                ViewBag.會員大頭貼 = new 資料產生器().會員頭貼(SSID);
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult deletebook(int fid)
        {
            new 資料儲存器().預約刪除(fid);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult sendscore(int fid,int fScore, string fComment)
        {
            string result = new 資料儲存器().評價產生(fid, fScore, fComment);
            return Json(new { Message = result },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Headupbdate(string filename, HttpPostedFileBase blob)
        {
            try
            {
                string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));
                var newfileName180 = GenerateFileName("Memberheadstack", systemFileExtenstion);
                var fullpath = "~/Images/" + newfileName180;
                blob.SaveAs(Server.MapPath(fullpath));

                int SSID = Convert.ToInt32(User.Identity.Name);
                tMember q = db.tMember.Where(m => m.fID == SSID).FirstOrDefault();
                q.fHeadstack = newfileName180;
                db.SaveChanges();
                return Json("OK");
            }
            catch (Exception)
            {
                return Json(new { Message = "Error" });
                throw;
            }

        }

        public string GenerateFileName(string fileTypeName, string fileextenstion)
        {
            if (fileTypeName == null) throw new ArgumentNullException(nameof(fileTypeName));
            if (fileextenstion == null) throw new ArgumentNullException(nameof(fileextenstion));
            return $"{fileTypeName}_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{fileextenstion}";
        }
    }
}