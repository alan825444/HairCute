using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 預約查詢類別
    {
        public Nullable<System.TimeSpan> startTime { get; set; }
        public Nullable<System.TimeSpan> closeTime { get; set; }
        public List<已預約資料類別> bookTimeList { get; set; }
    }
}