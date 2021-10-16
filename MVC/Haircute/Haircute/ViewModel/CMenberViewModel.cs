using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Haircute.ViewModel
{
    public class CMenberViewModel
    {
        public tMember Member { get; set; }

        public CMenberViewModel() 
        {
            this.Member = new tMember();
        }
        public int fID
        {
            get { return this.Member.fID; }
            set { this.Member.fID = value; }
        }
        public string fPwd 
        {
            get { return this.Member.fPwd; }
            set { this.Member.fPwd = value; }
        }
        public string fUsername 
        {
            get { return this.Member.fUsername; }
            set { this.Member.fUsername = value; }
        }
        public string fEmail 
        {
            get { return this.Member.fEmail; }
            set { this.Member.fEmail = value; }
        }
        public string fPhone 
        {
            get { return this.Member.fPhone; }
            set { this.Member.fPhone = value; }
        }
        public string fGender 
        {
            get { return this.Member.fGender; }
            set { this.Member.fGender = value; }
        }
        public Nullable<System.DateTime> fBirth 
        {
            get { return this.Member.fBirth; }
            set { this.Member.fBirth = value; }
        }
        public string fArea 
        {
            get { return this.Member.fArea; }
            set { this.Member.fArea = value; }
        }
        public string fDesign 
        {
            get { return this.Member.fDesign; }
            set { this.Member.fDesign = value; }
        }
        public Nullable<int> fk_Designer { get; set; }
        public Nullable<int> fk_Memorder { get; set; }
        public string fNickname 
        {
            get { return this.Member.fNickname; }
            set { this.Member.fNickname = value; }
        }
        public string fCity 
        {
            get { return this.Member.fCity; }
            set { this.Member.fCity = value; }
        }
    }
}