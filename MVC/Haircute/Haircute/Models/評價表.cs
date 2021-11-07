using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 評價表
    {
        public int fid { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public string MemberName { get; set; }
        public string Service { get; set; }
        public DateTime date { get; set; }
       
    }
}