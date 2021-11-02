using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 設計師個人資料
    {
        public string 設計師姓名 { get; set; }
        public string 店名 { get; set; }
        public string 地址 { get; set; }
        public string 大頭貼 { get; set; }
    }
    
    public class 設計師服務品項
    {
        public string 服務項目 { get; set; }
        public string 價格 { get; set; }
    }
}