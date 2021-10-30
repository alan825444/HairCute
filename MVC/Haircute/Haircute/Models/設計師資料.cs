using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 設計師資料
    {
        demodbEntities db = new demodbEntities();

        public int 取得設計師ID(int ID)
        {
            var q = db.tDesigner.Where(m => m.fk_Member == ID).FirstOrDefault();
            return (q.fid);
        }

        public 設計師個人資料 設計師頁面卡片(int ID) 
        {
            var 設計師ID = new 設計師資料().取得設計師ID(ID);
            設計師個人資料 data = new 設計師個人資料();
            var q = db.tMember.Join(
                db.tDesigner,
                m =>m.fID,
                d =>d.fk_Member,
                (m,d)=>new {
                  ID = m.fID,
                  會員表 = m,
                  設計師表 = d
                }).Where(md=>md.ID==ID).FirstOrDefault();
            data = new 設計師個人資料() 
            {
                設計師姓名 = q.會員表.fNickname,
                店名 = q.設計師表.fStore,
                地址 = q.設計師表.fAddress,
                大頭貼 = q.設計師表.fHeadSticker
            };
            return data;
        }

        public List<設計師服務品項> 設計師服務品項(int ID)
        {
            int 設計師ID = new 設計師資料().取得設計師ID(ID);
            List<設計師服務品項> data = new List<設計師服務品項>();
            var q = db.tService.Where(m => m.fk_Designer == 設計師ID).Select(m=>new 
            {
                服務 = m.fServicN,
                價格 = m.fprice
            });
            foreach (var item in q)
            {
                data.Add(new 設計師服務品項 { 
                    服務項目 = item.服務,
                    價格 = item.價格
                });   
            }
            
            return data;
        }



    }
}