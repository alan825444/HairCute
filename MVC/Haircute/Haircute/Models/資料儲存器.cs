using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 資料儲存器
    {
        public string 店鋪及營業時間儲存(int SSID,設計師資料 data) 
        {
            try
            {
                if (data != null)
                {
                    demodbEntities db = new demodbEntities();
                    var q = db.tDesigner.Where(x => x.fk_Member == SSID).FirstOrDefault();
                    q.fStore = data.fStore;
                    q.fStoreCity = data.fCity;
                    q.fStoreArea = data.fArea;
                    q.fAddress = data.fAddress;
                    var q2 = db.tWork.Where(x => x.fk_Designer == q.fid).FirstOrDefault();
                    if (q2 == null)
                    {
                        tWork datawork = new tWork();
                        datawork = new tWork { fStartTime = data.fStartTime, fEndTime = data.fEndTime, fk_Designer = q.fid };
                        db.tWork.Add(datawork);
                        db.SaveChanges();
                    }
                    else
                    {
                        q2.fStartTime = data.fStartTime;
                        q2.fEndTime = data.fEndTime;
                        db.SaveChanges();
                    }

                    return "OK";
                }

                return "False";
            }
            catch (Exception exe)
            {

                throw;
            }
            

        }

        public string 項目儲存 (int SSID, List<Service> data)
        {
            demodbEntities db = new demodbEntities();
            var q = db.tDesigner.Where(x => x.fk_Member == SSID).FirstOrDefault();
            foreach (var item in data)
            {
                if ((item.ID == null) && (item.Item != null) && (item.Price != null))
                {
                    db.tService.Add(new tService { fk_Designer = q.fid, fServicN = item.Item, fprice = item.Price });
                    db.SaveChanges();
                }
                else if ((item.ID != null) && (item.Item != null) && (item.Price != null))
                {
                    int id = Convert.ToInt32(item.ID);
                    db.tService.Where(x => x.fid == id).FirstOrDefault().fServicN = item.Item;
                    db.tService.Where(x => x.fid == id).FirstOrDefault().fprice = item.Price;
                    db.SaveChanges();
                }
                else if ((item.ID == null) && (item.Item == null) && (item.Price == null))
                {
                    db.tService.Add(new tService { fk_Designer = q.fid, fServicN = item.Item, fprice = item.Price });
                    db.SaveChanges();
                }
            }
            return "ok";
        }

        public string 會員資料修改(int SSID, tMember m) 
        {
            demodbEntities db = new demodbEntities();
            var q = db.tMember.Where(x => x.fID == SSID).FirstOrDefault();
            //"fUsername": 名稱, "fGender": 性別, "fNickname": 暱稱, "fBirth": 生日, "fCity": 城市, "fArea": 區域
            q.fUsername = m.fUsername;
            q.fGender = m.fGender;
            q.fNickname = m.fNickname;
            q.fBirth = m.fBirth;
            q.fCity = m.fCity;
            q.fArea = m.fArea;
            db.SaveChanges();
            return "OK";
        }

        public string 密碼修改(int SSID, string Pwd)
        {
            
            try
            {
                demodbEntities db = new demodbEntities();
                var q = db.tMember.Where(x => x.fID == SSID).FirstOrDefault();
                q.fPwd = Pwd;
                int result = db.SaveChanges();
                return "OK";
            }
            catch (Exception)
            {
                return "Error";
                throw;
            }
            
            
            
        }

        public string 刪除作品(int id)
        {
            try
            {
                demodbEntities db = new demodbEntities();
                var q = db.tPhoto.Where(x => x.fid == id).FirstOrDefault();
                string deletePhotoPath = q.fPath;
                db.tPhoto.Remove(q);
                db.SaveChanges();
                return deletePhotoPath;
            }
            catch (Exception)
            {
                return "Error";
                throw;
            }
            
        }
    }
}