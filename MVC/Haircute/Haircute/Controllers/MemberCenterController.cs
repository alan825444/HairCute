using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haircute.Models;
using System.IO;

namespace Haircute.Controllers
{
    public class MemberCenterController : Controller
    {
        // GET: MemberCenter
        demodbEntities db = new demodbEntities();

        public ActionResult Index()
        {
            int SSID = Convert.ToInt32(User.Identity.Name);
            var q = db.tMember.Where(x => x.fID == SSID).FirstOrDefault();
            if (q.fDesign == "F         ")
            {
                return RedirectToAction("Index", "UserCenter");
            }
            else
            {
                ViewBag.Name = q.fNickname;
                ViewBag.HImg = new 資料產生器().取得會員大頭貼(SSID);
                ViewBag.Dphoto = new 資料產生器().取得設計師作品(SSID);
                ViewBag.selectCity = new 資料產生器().selectedCity(SSID);
                ViewBag.selectArea = new 資料產生器().selectedArea(SSID);
                ViewBag.selectCity2 = new 資料產生器().取得會員City(SSID);
                ViewBag.selectArea2 = new 資料產生器().取得會員Area(SSID);
                ViewBag.店鋪資訊 = new 資料產生器().店鋪資訊(SSID);
                ViewBag.服務項目 = new 資料產生器().服務項目(SSID);
                ViewBag.會員資料 = new 資料產生器().取得會員資料(SSID);
                ViewBag.預約紀錄 = new 資料產生器Test().預約(SSID);
                return View();
            }
            
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

                var DID = db.tDesigner.Where(m => m.fk_Member == SSID).FirstOrDefault();
                tPhoto data = new tPhoto() { fk_Designer = DID.fid, fPath = newfileName180, fTag = Keyword, fDateTime = DateTime.Now };
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
            try
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

        [HttpPost]
        public ActionResult ServiceItem(List<Service> data)
        {
            int SSID = Convert.ToInt32(User.Identity.Name);
            new 資料儲存器().項目儲存(SSID, data);
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MemberDataUpdate(tMember m) 
        {
            int SSID = Convert.ToInt32(User.Identity.Name);
            string result = new 資料儲存器().會員資料修改(SSID, m);
            return Json(new { Message = result});
        }

        [HttpPost]
        public ActionResult PwdChange(string fpwd)
        {
            int SSID = Convert.ToInt32(User.Identity.Name);
            string resault = new 資料儲存器().密碼修改(SSID, fpwd);
            return Json(new { Message = resault});
        }

        [HttpPost]
        public ActionResult deletephoto(int fid) 
        {
            var fullpath = "~/Images/" + new 資料儲存器().刪除作品(fid);
            System.IO.File.Delete(Server.MapPath(fullpath));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult deletebook(int fid)
        {
            new 資料儲存器().預約刪除(fid);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult sendscore(int fid, int fScore, string fComment)
        {
            string result = new 資料儲存器().評價產生(fid, fScore, fComment);
            return Json(new { Message = result }, JsonRequestBehavior.AllowGet);
        }
    }
}