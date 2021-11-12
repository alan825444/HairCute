using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 預約資料類別
    {
        public int fDid { get; set; }
        public int fSeverid { get; set; }
        public int fMemberid { get; set; }
        public string fName { get; set; }
        public string fPhone { get; set; }
        public Nullable<System.DateTime> fDateTime { get; set; }
        public Nullable<System.TimeSpan> fBookTime { get; set; }
        public string fstatus { get; set; }
    }
}