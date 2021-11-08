using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.Models
{
    public class 照片資料
    {
        public int id { get; set; }
        public int DesignerId { get; set; }
        public string DesignerName { get; set; }
        public string photo { get; set; }
        public string tag { get; set; }
        public string photo2 { get; set; }
        public string tag2 { get; set; }
        public string fTime { get; set; }
    }
}