﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLTP.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLTP_Entities : DbContext
    {
        public QLTP_Entities()
            : base("name=QLTP_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Bill_detail> Bill_detail { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Discount_by_Rank> Discount_by_Rank { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Product_Item> Product_Item { get; set; }
        public virtual DbSet<Product_type> Product_type { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
    }
}
