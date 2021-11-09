using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 預約紀錄
    {
        public int 預約ID { get; set; }
        public int 設計師ID { get; set; }
        public string 設計師照 { get; set; }
        public string 設計師名 { get; set; }
        public string 設計師電話 { get; set; }
        public string 預約服務 { get; set; }
        public string 價格 { get; set; }
        public DateTime 比較時間 { get; set; }
        public string 預約日期 { get; set; }
        public string 預約時間 { get; set; }
    }
}