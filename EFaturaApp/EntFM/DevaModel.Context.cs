﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFaturaApp.EntFM
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EKSPRES2017Entities : DbContext
    {
        public EKSPRES2017Entities()
            : base("name=EKSPRES2017Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<fatura> fatura { get; set; }
        public virtual DbSet<faturahar> faturahar { get; set; }
        public virtual DbSet<irsaliye> irsaliye { get; set; }
        public virtual DbSet<krhatsub> krhatsub { get; set; }
        public virtual DbSet<krmuste> krmuste { get; set; }
        public virtual DbSet<tesellum> tesellum { get; set; }
        public virtual DbSet<tesellumhar> tesellumhar { get; set; }
    }
}
