using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 設計師資料
    {
        public int fid { get; set; }
        public string fStore { get; set; }
        public int fCity { get; set; }
        public int fArea { get; set; }
        public string fAddress { get; set; }
        public Nullable<System.TimeSpan> fStartTime { get; set; }
        public Nullable<System.TimeSpan> fEndTime { get; set; }
    }
}