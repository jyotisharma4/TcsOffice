﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Entity;

namespace TCSOffice.Business.DataAccess
{
    public class TCSOfficeDbContext : DbContext
    {
        public TCSOfficeDbContext()
            : base("name=TCSOfficeContext")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Company>()
                .Property(t => t.CompanyName)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            modelBuilder
                .Entity<Login>()
                .Property(t => t.UserName)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            modelBuilder
                .Entity<Login>()
                .Property(t => t.Email)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<DailySalesReports> DailySalesReports { get; set; }
        public DbSet<ZedInfo> ZedInfo { get; set; }
        public DbSet<CashInvoiceSale> CashInvoiceSale { get; set; }
        public DbSet<ChequesInfo> ChequesInfo { get; set; }
        public DbSet<CreditSales> CreditSales { get; set; }
        public DbSet<ReceiptOfTheDay> ReceiptOfTheDay { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
