﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<TB_CATEGORY> TB_CATEGORY { get; set; }
        public DbSet<TB_GROUP> TB_GROUP { get; set; }
        public DbSet<TB_GROUP_USER> TB_GROUP_USER { get; set; }
        public DbSet<TB_PRODUCT> TB_PRODUCT { get; set; }
        public DbSet<TB_PRODUCT_LOG> TB_PRODUCT_LOG { get; set; }
        public DbSet<TB_SUBCATEGORY> TB_SUBCATEGORY { get; set; }
        public DbSet<TB_USER> TB_USER { get; set; }
        public DbSet<VW_PRODUCT_CATEGORIES> VW_PRODUCT_CATEGORIES { get; set; }
    }
}
