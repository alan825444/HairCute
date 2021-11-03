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
        demodbEntities db = new demodbEntities();

        public ActionResult Index()
        {
            int SSID = Convert.ToInt32(User.Identity.Name);
            
            ViewBag.HImg = new 資料產生器().取得會員大頭貼(SSID);
            ViewBag.Dphoto = new 資料產生器().取得設計師作品(SSID);
            ViewBag.selectCity = new 資料產生器().selectedCity(SSID);
            ViewBag.selectArea = new 資料產生器().selectedArea(SSID);
            ViewBag.店鋪資訊 = new 資料產生器().店鋪資訊(SSID);
            return View();
        }

        [HttpPost]
        public ActionResult photoupdate(string filename, HttpPostedFileBase blob ,string Keyword)
        {
            try
            {
                int SSID = Convert.ToInt32(User.Identity.Name);
                string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));
                var newfileName180 = GenerateFileName("PhotoDesigner", systemFileExtenstion);
                var fullpath = "~/Images/" + newfileName180;
                blob.SaveAs(Server.MapPath(fullpath));

                var q = db.tPhoto.Where(m => m.fk_Designer == SSID).FirstOrDefault();
                tPhoto data = new tPhoto() { fk_Designer = SSID, fPath = newfileName180, fTag = Keyword, fDateTime = DateTime.Now };
                db.tPhoto.Add(data);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error" });
                throw;
                
            }
            
            return Json(new { Message = "OK" });
        }

        public ActionResult Headupbdate(string filename, HttpPostedFileBase blob)
        {
            string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));
            var newfileName180 = GenerateFileName("PhotoDesigner", systemFileExtenstion);
            var fullpath = "~/Images/" + newfileName180;
            blob.SaveAs(Server.MapPath(fullpath));

            int SSID = Convert.ToInt32(User.Identity.Name);
            tDesigner q = db.tDesigner.Where(m => m.fk_Member == SSID).FirstOrDefault();
            q.fHeadSticker = newfileName180;
            db.SaveChanges();
            return Json(new { Message = "OK" });
        }

        public string GenerateFileName(string fileTypeName, string fileextenstion)
        {
            if (fileTypeName == null) throw new ArgumentNullException(nameof(fileTypeName));
            if (fileextenstion == null) throw new ArgumentNullException(nameof(fileextenstion));
            return $"{fileTypeName}_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{fileextenstion}";
        }
        [HttpPost]
        public ActionResult StoreBasicInf(設計師資料 m) 
        {
            int SSID = Convert.ToInt32(User.Identity.Name);
            string result = new 資料儲存器().店鋪及營業時間儲存(SSID, m);

            if (result =="OK")
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "儲存失敗";
            return RedirectToAction("Index");



        }
    }
}