namespace TCSOffice.Business.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailidunique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logins", "Email", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Logins", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Logins", new[] { "Email" });
            AlterColumn("dbo.Logins", "Email", c => c.String());
        }
    }
}
