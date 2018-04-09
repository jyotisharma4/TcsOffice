namespace TCSOffice.Business.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Login_Company_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 256),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        DateArchived = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        DateDeactivated = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedBy = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CompanyName, unique: true);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
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
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "Company_Id", "dbo.Companies");
            DropIndex("dbo.Logins", new[] { "Company_Id" });
            DropIndex("dbo.Logins", new[] { "UserName" });
            DropIndex("dbo.Companies", new[] { "CompanyName" });
            DropTable("dbo.Logins");
            DropTable("dbo.Companies");
        }
    }
}
