﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopDunk.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBShopDunkEntities1 : DbContext
    {
        public DBShopDunkEntities1()
            : base("name=DBShopDunkEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<Battery> Batteries { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FrontCamera> FrontCameras { get; set; }
        public virtual DbSet<Memory> Memories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<ProDetail> ProDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RearCamera> RearCameras { get; set; }
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
    }
}