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
            return View();
        }

        [HttpPost]
        public ActionResult photoupdate(string filename, HttpPostedFileBase blob)
        {
            string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));
            var newfileName180 = GenerateFileName("PhotoDesigner", systemFileExtenstion);
            var fullpath = "~/Images/" + newfileName180;
            blob.SaveAs(Server.MapPath(fullpath));
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