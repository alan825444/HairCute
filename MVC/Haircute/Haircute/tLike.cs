//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Haircute
{
    using System;
    using System.Collections.Generic;
    
    public partial class tLike
    {
        public int fid { get; set; }
        public string fLike { get; set; }
        public Nullable<int> fk_Photo { get; set; }
        public Nullable<int> fk_Member { get; set; }
    
        public virtual tMember tMember { get; set; }
        public virtual tPhoto tPhoto { get; set; }
    }
}