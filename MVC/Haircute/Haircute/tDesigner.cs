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
    
    public partial class tDesigner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tDesigner()
        {
            this.tMember = new HashSet<tMember>();
            this.tService = new HashSet<tService>();
        }
    
        public int fid { get; set; }
        public string fStore { get; set; }
        public string fAddress { get; set; }
        public Nullable<int> fk_Member { get; set; }
        public string fHeadSticker { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tMember> tMember { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tService> tService { get; set; }
        public virtual tMember tMember1 { get; set; }
    }
}
