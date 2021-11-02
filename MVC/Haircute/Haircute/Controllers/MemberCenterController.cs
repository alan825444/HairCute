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
            tDesigner q = db.tDesigner.Where(m => m.fk_Member == SSID).FirstOrDefault();
            string HeadImg = q.fHeadSticker;

            var q2 = db.tPhoto.Where(m => m.fk_Designer == SSID);
            List<tPhoto> data = new List<tPhoto>();
            foreach (var item in q2)
            {
                data.Add(new tPhoto { fk_Designer = item.fk_Designer, fPath = item.fPath, fTag = item.fTag, fDateTime = item.fDateTime });
            }

            ViewBag.HImg = HeadImg;
            ViewBag.Dphoto = data;
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
    }
}