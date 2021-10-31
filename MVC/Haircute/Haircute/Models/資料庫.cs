using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Haircute.Models
{
    public static class 資料庫
    {
        public static void 新增<某類別>(某類別 新物件) where 某類別 : class
        {
            demodbEntities 資料庫 = new demodbEntities();

            資料庫.Set<某類別>().Add(新物件);

            資料庫.SaveChanges();
        }


        public static List<某類別> 查詢<某類別>() where 某類別 : class
        {
            demodbEntities 資料庫 = new demodbEntities();

            return 資料庫.Set<某類別>().ToList();
        }

        public static 某類別 查詢<某類別>(object 主索引) where 某類別 : class
        {
            demodbEntities 資料庫 = new demodbEntities();

            return 資料庫.Set<某類別>().Find(主索引);
        }


        public static void 修改<某類別>(int 主索引, Action<某類別> 修改流程) where 某類別 : class, new()
        {
            demodbEntities 資料庫 = new demodbEntities();

            某類別 物件 = 查詢<某類別>(主索引);

            資料庫.Set<某類別>().Attach(物件);

            修改流程(物件);

            資料庫.SaveChanges();
        }
        // 使用範例: 修改 tMember ID 為 1002 的 fPhone
        //
        // 資料庫.修改<tMember>(1002, 會員 => {
        //     會員.fPhone = "0988888888";
        // });


        public static void 刪除<某類別>(object 主索引) where 某類別 : class
        {
            demodbEntities 資料庫 = new demodbEntities();

            資料庫.Set<某類別>().Remove(資料庫.Set<某類別>().Find(主索引));

            資料庫.SaveChanges();
        }


        // 基於 entity framework 的傳統 SQL 方法
        public static List<某類別> 查詢<某類別>(string 查詢條件) where 某類別 : class
        {
            demodbEntities 資料庫 = new demodbEntities();

            string 查詢字串 = $"SELECT * FROM {typeof(某類別).Name}";

            if (!string.IsNullOrEmpty(查詢條件))
                查詢字串 += $" {查詢條件}";

            return 資料庫.Database.SqlQuery<某類別>(查詢字串).ToList();
        }
    }
}