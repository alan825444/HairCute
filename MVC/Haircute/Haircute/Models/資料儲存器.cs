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
    }
}