using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haircute.ViewModel;

namespace Haircute.Models
{

    public class 用戶crud
    {
        demodbEntities db = new demodbEntities();

        public bool 註冊(CMenberViewModel m)
        {
            var 註冊 = db.tMember.Where(c => c.fEmail == m.fEmail).FirstOrDefault();
            if (註冊 == null)
            {
                db.tMember.Add(m.Member);
                db.SaveChanges();
                return true;
            }
            return false;

        }



        public 用戶 用戶資料(int ID)
        {
            List<用戶> list = new List<用戶>();
            var 用戶資料 = db.tMember.Where(m => m.fID == ID).FirstOrDefault();
            list.Add(new 用戶
            {
                fID = 用戶資料.fID,
                fPwd = 用戶資料.fPwd,
                fUsername = 用戶資料.fUsername,
                fEmail = 用戶資料.fEmail,
                fPhone = 用戶資料.fPhone,
                fGender = 用戶資料.fGender,
                fBirth = 用戶資料.fBirth,
                fArea = 用戶資料.fArea,
                fCity = 用戶資料.fCity,
                fconfirmation = 用戶資料.fconfirmation,
                fNickname = 用戶資料.fNickname,
                fk_Designer = 用戶資料.fk_Designer,
                fk_Memorder = 用戶資料.fk_Memorder

            });
            return list[0];
        }

        

        
    }
}