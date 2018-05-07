namespace TCSOffice.Business.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        ContributionNumber = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        MobileNumber = c.Int(nullable: false),
                        OpeningBalance = c.Int(nullable: false),
                        CreditLimit = c.Int(nullable: false),
                        StartCreditPeriod = c.DateTime(),
                        EndCreditPeriod = c.DateTime(),
                        DGEApproved = c.Boolean(),
                        FaxNumber = c.Int(nullable: false),
                        Attendent = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Company_Id", "dbo.Companies");
            DropIndex("dbo.Customers", new[] { "Company_Id" });
            DropTable("dbo.Customers");
        }
    }
}
