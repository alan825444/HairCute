using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Haircute.Models
{
    public class 資料產生器
    {
        demodbEntities db = new demodbEntities();
        public string 取得會員大頭貼(int SSID)
        {
            tDesigner q = db.tDesigner.Where(m => m.fk_Member == SSID).FirstOrDefault();
            if (q == null)
            {
                return "nothing";
            }
            return q.fHeadSticker;
            
        }

        public List<tPhoto> 取得設計師作品(int SSID) 
        {
            var q = db.tDesigner.Where(m => m.fk_Member == SSID).FirstOrDefault();
            var q2 = db.tPhoto.Where(m => m.fk_Designer == q.fid);
            List<tPhoto> data = new List<tPhoto>();

            foreach (var item in q2)
            {
                data.Add(new tPhoto { fk_Designer = item.fk_Designer, fPath = item.fPath, fTag = item.fTag, fDateTime = item.fDateTime });
            }

            return data;
         
            
        }

        public List<SelectListItem> selectedCity(int SSID) 
        {
            var city = db.tCity.OrderBy(m => m.fID);
            List<SelectListItem> data = new List<SelectListItem>();
            foreach (var item in city)
            {
                data.Add(new SelectListItem { Value = item.fID.ToString(), Text = item.fCity });
            }
            tDesigner q = db.tDesigner.Where(m => m.fk_Member == SSID).FirstOrDefault();
            if (q ==null)
            {
                return data;
            }
            else
            {
                if ((q.fStoreCity != null) || (q.fStoreArea != null))
                {
                    data.Where(x => x.Value == q.fStoreCity.ToString()).First().Selected = true;

                    return data;
                }
            }
            
            return data;
        }

        public List<SelectListItem> selectedArea(int SSID)
        {
            List<SelectListItem> data = new List<SelectListItem>();
            tDesigner q = db.tDesigner.Where(m => m.fk_Member == SSID).FirstOrDefault();
            if (q !=null)
            {
                if ((q.fStoreCity == null) || (q.fStoreArea == null))
                {
                    return data;
                }
                else
                {
                    var area = db.tArea.Where(m => m.fk_City == q.fStoreCity).OrderBy(m => m.fid);
                    foreach (var item in area)
                    {
                        data.Add(new SelectListItem { Value = item.fid.ToString(), Text = item.fArea });
                    }
                    data.Where(x => x.Value == q.fStoreArea.ToString()).First().Selected = true;
                    return data;
                }
            }
            return data;
            
        }

        public List<設計師資料> 店鋪資訊(int SSID)
        {
            var q = db.tDesigner.Where(x => x.fk_Member == SSID).FirstOrDefault();
            List<設計師資料> data = new List<設計師資料>();
            if (q !=null)
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
        
        
        
    }
}