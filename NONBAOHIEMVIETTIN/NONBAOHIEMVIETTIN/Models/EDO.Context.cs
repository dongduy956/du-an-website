﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NONBAOHIEMVIETTIN.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class nonbaohiemviettinEntities : DbContext
    {
        public nonbaohiemviettinEntities()
            : base("name=nonbaohiemviettinEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<accounts> accounts { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<contact> contact { get; set; }
        public virtual DbSet<counonline> counonline { get; set; }
        public virtual DbSet<groupproduct> groupproduct { get; set; }
        public virtual DbSet<introduce> introduce { get; set; }
        public virtual DbSet<listimage> listimage { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<newstype> newstype { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<orderdetail> orderdetail { get; set; }
        public virtual DbSet<production> production { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<rate> rate { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<sendcontactinfo> sendcontactinfo { get; set; }
        public virtual DbSet<subscribe> subscribe { get; set; }
    }
}
