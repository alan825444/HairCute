﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class demodbEntities : DbContext
    {
        public demodbEntities()
            : base("name=demodbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tMember> tMember { get; set; }
        public virtual DbSet<tArea> tArea { get; set; }
        public virtual DbSet<tCity> tCity { get; set; }
        public virtual DbSet<tService> tService { get; set; }
        public virtual DbSet<tBook> tBook { get; set; }
        public virtual DbSet<tComment> tComment { get; set; }
        public virtual DbSet<tLike> tLike { get; set; }
        public virtual DbSet<tPhoto> tPhoto { get; set; }
        public virtual DbSet<tWork> tWork { get; set; }
        public virtual DbSet<tDesigner> tDesigner { get; set; }
    }
}
