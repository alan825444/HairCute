using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 資料產生器Test
    {
        demodbEntities db = new demodbEntities();

        public List<設計師資料> 設計師店鋪(int SSID)
        {
            var q = db.tDesigner.Where(x => x.fid == SSID).FirstOrDefault();
            List<設計師資料> data = new List<設計師資料>();
            if (q != null)
            {
                var q2 = db.tWork.Where(x => x.fk_Designer == q.fid).FirstOrDefault();


                if (q.fAddress == null)
                {
                    return data;
                }
                else
                {
                    data.Add(new 設計師資料 { fStore = q.fStore, fAddress = q.fAddress, fStartTime = q2.fStartTime, fEndTime = q2.fEndTime });
                    return data;
                }
            }
            return data;
        }
        public List<tPhoto> 取得設計師作品(int SSID)
        {
            var q = db.tDesigner.Where(m => m.fid == SSID).FirstOrDefault();
            var q2 = db.tPhoto.Where(m => m.fk_Designer == q.fid);
            List<tPhoto> data = new List<tPhoto>();

            foreach (var item in q2)
            {
                data.Add(new tPhoto { fid = item.fid, fk_Designer = item.fk_Designer, fPath = item.fPath, fTag = item.fTag, fDateTime = item.fDateTime });
            }
            return data;

        }
        public string 取得會員大頭貼(int SSID)
        {
            tDesigner q = db.tDesigner.Where(m => m.fid == SSID).FirstOrDefault();
            if (q == null)
            {
                return "nothing";
            }
            return q.fHeadSticker;

        }
        public List<Service> 服務項目(int SSID)
        {
            var q = db.tDesigner.Where(x => x.fid == SSID).FirstOrDefault();
            List<Service> data = new List<Service>();
            var q2 = db.tService.Where(x => x.fk_Designer == q.fid);
            if (q2 != null)
            {
                foreach (var item in q2)
                {
                    data.Add(new Service { ID = item.fid.ToString(), Item = item.fServicN, Price = item.fprice, DesignerId = q.fid });
                }
            }
            return data;
        }
        public List<評價表> 評價(int SSID)
        {
            var q = db.tDesigner.Where(m => m.fid == SSID).FirstOrDefault();
            List<評價表> data = new List<評價表>();
            var q2 = from x in db.tComment
                     where x.fk_Designer == q.fid
                     join m in db.tMember on x.fk_Member equals m.fID
                     join b in db.tBook on x.fk_Book equals b.fid
                     join s in db.tService on b.fk_Service equals s.fid
                     select new
                     {
                         x.fid,x.fScore,x.fComment,m.fNickname,b.fDateTime,s.fServicN
                     };
                     
            if (q2 != null)
            {
                foreach (var item in q2)
                {
                    data.Add(new 評價表 { fid = item.fid, Score = (int)item.fScore, 
                        Comment = item.fComment,MemberName=item.fNickname,
                        Service=item.fServicN,date= (DateTime)item.fDateTime/*).ToString("d")*/});
                    //DateTime thisdate = new DateTime();
                    //((DateTime)item.fDateTime).ToString("d");
                    
                }
                
            }
            return data;

        }

        public List<預約紀錄> 預約(int SSID)
        {
            var q = db.tMember.Where(m => m.fID == SSID).FirstOrDefault();
            var q2 = from b in db.tBook
                     where b.fk_Member == q.fID
                     join s in db.tService on b.fk_Service equals s.fid
                     join d in db.tDesigner on b.fk_Designer equals d.fid
                     join m in db.tMember on d.fk_Member equals m.fID
                     select new
                     {
                         b.fid,
                         s.fk_Designer,
                         d.fHeadSticker,
                         m.fNickname,
                         m.fPhone,
                         s.fServicN,
                         s.fprice,
                         b.fDateTime,
                         b.fBookTime,
                         b.fstatus
                     };
            List<預約紀錄> data = new List<預約紀錄>();
            if(q2 != null)
            {
                foreach (var item in q2)
                {
                    data.Add(new 預約紀錄
                    {
                        預約ID = item.fid,
                        設計師ID = (int)item.fk_Designer,
                        設計師照 = item.fHeadSticker,
                        設計師名 = item.fNickname,
                        設計師電話 = item.fPhone,
                        預約服務 = item.fServicN,
                        價格 = item.fprice,
                        比較時間 = (DateTime)item.fDateTime,
                        預約日期 = ((DateTime)item.fDateTime).ToString("d"),
                        預約時間 = ((TimeSpan)item.fBookTime).ToString(@"hh\:mm"),
                        狀態=item.fstatus
                    });
                }
            }
            return data;
        }

    }
}