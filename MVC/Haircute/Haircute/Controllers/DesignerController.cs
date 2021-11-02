using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haircute.Models;
using Haircute.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Haircute.Controllers
{
    public class DesignerController : Controller
    {
        demodbEntities db = new demodbEntities();
        // GET: Designer
        public ActionResult Index(int fid)
        {
            ViewBag.Name = db.tMember.Where(m => m.fk_Designer == fid).FirstOrDefault().fNickname;

            設計師主頁ViewModel de = new 設計師主頁ViewModel()
            {
                設計師表 = db.tDesigner.Where(m => m.fid == fid).ToList(),
                服務表 = db.tService.Where(m => m.fk_Designer == fid).ToList(),
                評價Tab = 評價(fid),
                預約Tab = 預約(fid)
            };
            return View(de);
        }

        public List<評價表> 評價(int fid)
        {
            var eva = (from q in db.tEvaluate
                       where fid == q.fk_Designer 
                       join m in db.tMember on q.fk_Member equals m.fID //送評價的人
                       join s in db.tService on q.fk_Service equals s.fid //取服務名
                       join b in db.tBook on q.fk_Order equals b.fid //取時間
                       select new 評價表
                       {
                           Star = (int)q.fStar,
                           Evaluate = q.fEvaluate,
                           Nickname = m.fNickname,
                           Service = s.fServicN,
                           Date = b.fDateTime.ToString()

                       }).ToList();
            return eva;
        }
        public List<預約表> 預約(int fid)
        {
            
            var res = (from d in db.tDesigner
                       where d.fid == fid
                       join b in db.tBook on d.fk_Member equals b.fk_Member //找到自己的預約
                       join m in db.tMember on b.fk_Designer equals m.fk_Designer //預約的設計師名字
                       join md in db.tDesigner on m.fk_Designer equals md.fid //預約的設計師店跟地址
                       join s in db.tService on b.fk_Service equals s.fid //預約的服務
                       select new 預約表
                       {
                           Name = m.fNickname,
                           Store = md.fStore,
                           Address = md.fAddress,
                           Service = s.fServicN,
                           Price = s.fprice,
                           Date=b.fDateTime.ToString()

                       }).ToList();
            return res;
        }
        
    }
}