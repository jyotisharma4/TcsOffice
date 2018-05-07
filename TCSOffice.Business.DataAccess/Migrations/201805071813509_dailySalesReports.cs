namespace TCSOffice.Business.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dailySalesReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CashInvoiceSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumberFrom = c.Int(nullable: false),
                        InvoiceNumberTo = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                        DailySalesReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DailySalesReports", t => t.DailySalesReport_Id)
                .Index(t => t.DailySalesReport_Id);
            
            CreateTable(
                "dbo.DailySalesReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DSRNumber = c.Int(nullable: false),
                        DSRDate = c.DateTime(nullable: false),
                        TotalSale = c.Int(nullable: false),
                        TotalReceiptAmount = c.Int(nullable: false),
                        CashReceived = c.Int(nullable: false),
                        TotalChequesReceived = c.Int(nullable: false),
                        TotalExpenses = c.Int(nullable: false),
                        TotalCreditSale = c.Int(nullable: false),
                        Remarks = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChequesInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChequeNumber = c.String(),
                        IssuedFrom = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        Remarks = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                        DailySalesReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DailySalesReports", t => t.DailySalesReport_Id)
                .Index(t => t.DailySalesReport_Id);
            
            CreateTable(
                "dbo.CreditSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        HT = c.String(),
                        TVA = c.Int(nullable: false),
                        Pre = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                        DailySalesReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DailySalesReports", t => t.DailySalesReport_Id)
                .Index(t => t.DailySalesReport_Id);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(),
                        Amount = c.Int(nullable: false),
                        Remarks = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                        DailySalesReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DailySalesReports", t => t.DailySalesReport_Id)
                .Index(t => t.DailySalesReport_Id);
            
            CreateTable(
                "dbo.ReceiptOfTheDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiptNumber = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Remarks = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                        DailySalesReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DailySalesReports", t => t.DailySalesReport_Id)
                .Index(t => t.DailySalesReport_Id);
            
            CreateTable(
                "dbo.ZedInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZedNumber = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                        DailySalesReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DailySalesReports", t => t.DailySalesReport_Id)
                .Index(t => t.DailySalesReport_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZedInfoes", "DailySalesReport_Id", "dbo.DailySalesReports");
            DropForeignKey("dbo.ReceiptOfTheDays", "DailySalesReport_Id", "dbo.DailySalesReports");
            DropForeignKey("dbo.Expenses", "DailySalesReport_Id", "dbo.DailySalesReports");
            DropForeignKey("dbo.CreditSales", "DailySalesReport_Id", "dbo.DailySalesReports");
            DropForeignKey("dbo.ChequesInfoes", "DailySalesReport_Id", "dbo.DailySalesReports");
            DropForeignKey("dbo.CashInvoiceSales", "DailySalesReport_Id", "dbo.DailySalesReports");
            DropIndex("dbo.ZedInfoes", new[] { "DailySalesReport_Id" });
            DropIndex("dbo.ReceiptOfTheDays", new[] { "DailySalesReport_Id" });
            DropIndex("dbo.Expenses", new[] { "DailySalesReport_Id" });
            DropIndex("dbo.CreditSales", new[] { "DailySalesReport_Id" });
            DropIndex("dbo.ChequesInfoes", new[] { "DailySalesReport_Id" });
            DropIndex("dbo.CashInvoiceSales", new[] { "DailySalesReport_Id" });
            DropTable("dbo.ZedInfoes");
            DropTable("dbo.ReceiptOfTheDays");
            DropTable("dbo.Expenses");
            DropTable("dbo.CreditSales");
            DropTable("dbo.ChequesInfoes");
            DropTable("dbo.DailySalesReports");
            DropTable("dbo.CashInvoiceSales");
        }
    }
}
