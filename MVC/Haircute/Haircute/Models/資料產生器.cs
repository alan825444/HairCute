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
                data.Add(new tPhoto { fid=item.fid, fk_Designer = item.fk_Designer, fPath = item.fPath, fTag = item.fTag, fDateTime = item.fDateTime });
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

        public List<SelectListItem> 取得會員City(int SSID)
        {
            var city = db.tCity.OrderBy(m => m.fID);
            List<SelectListItem> data = new List<SelectListItem>();
            foreach (var item in city)
            {
                data.Add(new SelectListItem { Value = item.fID.ToString(), Text = item.fCity });
            }
            var 會員資料 = db.tMember.Where(x => x.fID == SSID).FirstOrDefault();
            if (會員資料==null)
            {
                return data;
            }
            else
            {
                data.Where(x => x.Value == 會員資料.fCity.ToString()).First().Selected = true;
                
                return data;
            }
        }

        public List<SelectListItem> 取得會員Area(int SSID)
        { 
            List<SelectListItem> data = new List<SelectListItem>();
            var 會員資料 = db.tMember.Where(x => x.fID == SSID).FirstOrDefault();
            int fcityID = Convert.ToInt32(會員資料.fCity);
            var area = db.tArea.Where(x=>x.fk_City == fcityID).OrderBy(x=>x.fid);
            if (會員資料 == null)
            {
                return data;
            }
            else
            {
                foreach (var item in area)
                {
                    data.Add(new SelectListItem { Value = item.fid.ToString(), Text = item.fArea });
                }
                data.Where(x => x.Value == 會員資料.fArea).First().Selected = true;
                return data;
            }
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

        public List<Service> 服務項目(int SSID) 
        {
            demodbEntities db = new demodbEntities();
            var q = db.tDesigner.Where(x => x.fk_Member == SSID).FirstOrDefault();
            List<Service> data = new List<Service>();
            var q2 = db.tService.Where(x => x.fk_Designer == q.fid);
            if (q2 != null)
            {
                foreach (var item in q2)
                {
                    data.Add(new Service { ID = item.fid.ToString(), Item = item.fServicN, Price = item.fprice });
                }
            }
            return data;
        }

        public List<tMember> 取得會員資料(int SSID)
        {
            demodbEntities de = new demodbEntities();
            var 會員資料 = db.tMember.Where(x => x.fID == SSID).FirstOrDefault();
            List<tMember> data = new List<tMember>() {
            new tMember
            {
                fUsername = 會員資料.fUsername,
                fGender = 會員資料.fGender,
                fNickname = 會員資料.fNickname,
                fBirth = 會員資料.fBirth,
            }};
            return data;

        }

        public List<照片資料> 取得最新上傳() 
        {
            demodbEntities db = new demodbEntities();
            List<int> ID = new List<int>();
            List<照片資料> data = new List<照片資料>();
            var q = db.tPhoto.OrderByDescending(x => x.fid).Take(30).GroupBy(x => x.fk_Designer).OrderBy(x => Guid.NewGuid()).Select(x=>new { 數量=x.Count() , ID = x.Key }).Take(5);
            foreach (var item in q)
            {
                if (item.數量>=2)
                {
                    ID.Add((int)item.ID);
                }
            }
            foreach (var item in ID)
            {
                var qphoto = db.tPhoto.Where(x => x.fk_Designer == item).OrderBy(x => x.fDateTime).Take(2).ToList();
                var qDesigner = db.tDesigner.Where(x => x.fid==item).FirstOrDefault();
                照片資料 temp = new 照片資料();
                temp = new 照片資料 { photo = qphoto[0].fPath, photo2 = qphoto[1].fPath, DesignerId = item ,DesignerName=qDesigner.tMember.fNickname };
                data.Add(temp);
            }
            return data;
        }

        public List<string> 取得關鍵字() 
        {
            demodbEntities db = new demodbEntities();
            List<string> data = new List<string>();
            var q = db.tPhoto.GroupBy(x=>x.fTag);
            foreach (var item in q)
            {
                data.Add(item.Key);
            }
            return data;
        }

        public List<搜尋資料> 回傳搜尋結果(int City, int Area ,string Keyword)
        {
            demodbEntities db = new demodbEntities();
            List<搜尋資料> data = new List<搜尋資料>();

            if (Keyword == "")
            {
                var q = db.tDesigner.Where(x => x.fStoreCity == City && x.fStoreArea == Area).GroupBy(x => x.fid);
                foreach (var item in q)
                {
                    搜尋資料 tempdata = new 搜尋資料();
                    var tempq = db.tDesigner.Where(x => x.fid == item.Key).FirstOrDefault();
                    int Cityid = Convert.ToInt32(tempq.fStoreCity);
                    int Areaid = Convert.ToInt32(tempq.fStoreArea);
                    string Address = db.tCity.Where(x => x.fID == Cityid).FirstOrDefault().fCity;
                    Address += db.tArea.Where(x => x.fid == Areaid).FirstOrDefault().fArea;
                    Address += tempq.fAddress;
                    tempdata.Photo1 = db.tPhoto.OrderBy(x => Guid.NewGuid()).FirstOrDefault().fPath;
                    tempdata.DesgnerName = db.tMember.Where(x=>x.fID == tempq.fk_Member).FirstOrDefault().fNickname;
                    tempdata.設計師ID = item.Key;
                    tempdata.Address = Address;
                    tempdata.HeadStack = tempq.fHeadSticker;
                    data.Add(tempdata);
                }
            }
            else
            {
                string[] KW = Keyword.Split('/');

                foreach (var item in KW)
                {
                    var q = db.tDesigner.Join(db.tPhoto, m => m.fid, s => s.fk_Designer,
                        (m, s) => new
                        {
                            Designer = m,
                            Photo = s
                        }).Where(x => x.Designer.fStoreCity == City && x.Designer.fStoreArea == Area && x.Photo.fTag == item);
                    var qGRBfid = q.GroupBy(x => x.Designer.fid);
                    foreach (var item2 in qGRBfid)
                    {
                        搜尋資料 tempdata = new 搜尋資料();
                        var tempq = q.Where(x => x.Designer.fid == item2.Key).FirstOrDefault();
                        int Cityid = Convert.ToInt32(tempq.Designer.fStoreCity);
                        int Areaid = Convert.ToInt32(tempq.Designer.fStoreArea);
                        string Address = db.tCity.Where(x => x.fID == Cityid).FirstOrDefault().fCity;
                        Address += db.tArea.Where(x => x.fid == Areaid).FirstOrDefault().fArea;
                        Address += tempq.Designer.fAddress;
                        tempdata.Photo1 = tempq.Photo.fPath;
                        tempdata.DesgnerName = tempq.Designer.tMember.fNickname;
                        tempdata.設計師ID = item2.Key;
                        tempdata.Address = Address;
                        tempdata.HeadStack = tempq.Designer.fHeadSticker;
                        data.Add(tempdata);
                    }

                }
            }

            return data;
            
        }


    }
}