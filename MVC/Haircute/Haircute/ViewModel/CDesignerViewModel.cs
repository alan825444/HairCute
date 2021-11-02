using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haircute.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Haircute.ViewModel
{
    public class 設計師主頁ViewModel
    {
        public List<tDesigner> 設計師表 { get; set; }
        public List<tMember> 會員表 { get; set; }
        public List<tService> 服務表 { get; set; }
        public List<評價表> 評價Tab { get; set; }
        public List<預約表> 預約Tab { get; set; }
    }
    public class 評價表
    {
        public int Star { get; set; }
        public string Evaluate { get; set; }
        public string Nickname { get; set; }
        public string Service { get; set; }
        public string Date { get; set; }
    }
    public class 預約表
    {
        public string 設計師大頭貼 { get; set; }
        public string Name { get; set; }
        public string Store { get; set; }
        public string Address { get; set; }
        public string Service { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
    }

    public class 設計師編輯
    {
        public string 設計師大頭貼 { get; set; }
        public string 店名 { get; set;}
        public string 縣市 { get; set; }
        public string 地區 { get; set; }
        public string 店址 { get; set; }
        public string 服務 { get; set; }
        public string 費用 { get; set; }
        
    }
}