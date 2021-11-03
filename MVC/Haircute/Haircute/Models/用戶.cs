using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 用戶
    {
        public int fID { get; set; }
        public string fPwd { get; set; }
        public string fUsername { get; set; }
        public string fEmail { get; set; }
        public string fPhone { get; set; }
        public string fGender { get; set; }
        public Nullable<System.DateTime> fBirth { get; set; }
        public string fArea { get; set; }
        public string fDesign { get; set; }
        public Nullable<int> fk_Designer { get; set; }
        public Nullable<int> fk_Memorder { get; set; }
        public string fNickname { get; set; }
        public string fCity { get; set; }
        public string fconfirmation { get; set; }
    }
}