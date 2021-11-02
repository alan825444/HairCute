using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("密碼")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "請確認密碼格式正確")]
        [Required(ErrorMessage = "此欄不得為空")]
        [DataType(DataType.Password )]
        public string fPwd 
        {
            get { return this.Member.fPwd; }
            set { this.Member.fPwd = value; }
        }
        [DisplayName("姓名")]
        [Required(ErrorMessage = "此欄不得為空")]
        public string fUsername 
        {
            get { return this.Member.fUsername; }
            set { this.Member.fUsername = value; }
        }
        [DisplayName("Email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",ErrorMessage ="請輸入正確的email格式")]
        [Required(ErrorMessage ="此欄不得為空")]
        public string fEmail 
        {
            get { return this.Member.fEmail; }
            set { this.Member.fEmail = value; }
        }
        [DisplayName("手機號碼")]
        [Required(ErrorMessage = "此欄不得為空")]
        public string fPhone 
        {
            get { return this.Member.fPhone; }
            set { this.Member.fPhone = value; }
        }
        [DisplayName("性別")]
        [Required(ErrorMessage = "此欄不得為空")]
        public string fGender 
        {
            get { return this.Member.fGender; }
            set { this.Member.fGender = value; }
        }
        [DisplayName("生日")]
        [RegularExpression(@"^((0?[1-9]|[12][0-9]|3[01])[/.](0?[1-9]|1[012])[/.]((?:19|20)[0-9]{2}))*$", ErrorMessage = "請輸入正確的日期格式")]
        [Required(ErrorMessage = "此欄不得為空")]
        public Nullable<System.DateTime> fBirth 
        {
            get { return this.Member.fBirth; }
            set { this.Member.fBirth = value; }
        }
        [DisplayName("地區")]
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
        [DisplayName("暱稱")]
        [Required(ErrorMessage = "此欄不得為空")]
        public string fNickname 
        {
            get { return this.Member.fNickname; }
            set { this.Member.fNickname = value; }
        }
        [DisplayName("城市")]
        public string fCity 
        {
            get { return this.Member.fCity; }
            set { this.Member.fCity = value; }
        }
    }
}