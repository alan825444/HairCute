﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haircute.Models;

namespace Haircute.Controllers
{
    public class StoreInfoController : Controller
    {
        // GET: StoreInfo
        demodbEntities db = new demodbEntities();
        public ActionResult Index(int? id)
        {
            /*int SSID = Convert.ToInt32(User.Identity.Name);*/
            int SSID = (int)id;
            var q = db.tDesigner.Where(x => x.fid == SSID).FirstOrDefault();
            ViewBag.Name = db.tMember.Where(x => x.fID == q.fk_Member).FirstOrDefault().fNickname;
            if(q != null)
            {
                
                ViewBag.HImg = new 資料產生器Test().取得會員大頭貼(SSID);
                ViewBag.Dphoto = new 資料產生器Test().取得設計師作品(SSID);
                ViewBag.店鋪資訊 = new 資料產生器Test().設計師店鋪(SSID);
                ViewBag.服務項目 = new 資料產生器Test().服務項目(SSID);
                ViewBag.評價 = new 資料產生器Test().評價(SSID);
                
            }
            return View();
        }
    }
}