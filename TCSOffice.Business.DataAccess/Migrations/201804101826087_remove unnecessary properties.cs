namespace TCSOffice.Business.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeunnecessaryproperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Address", c => c.String());
            DropColumn("dbo.Companies", "Address1");
            DropColumn("dbo.Companies", "Address2");
            DropColumn("dbo.Companies", "Address3");
            DropColumn("dbo.Companies", "City");
            DropColumn("dbo.Companies", "Country");
            DropColumn("dbo.Companies", "State");
            DropColumn("dbo.Companies", "Zip");
            DropColumn("dbo.Logins", "FirstName");
            DropColumn("dbo.Logins", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logins", "LastName", c => c.String());
            AddColumn("dbo.Logins", "FirstName", c => c.String());
            AddColumn("dbo.Companies", "Zip", c => c.String());
            AddColumn("dbo.Companies", "State", c => c.String());
            AddColumn("dbo.Companies", "Country", c => c.String());
            AddColumn("dbo.Companies", "City", c => c.String());
            AddColumn("dbo.Companies", "Address3", c => c.String());
            AddColumn("dbo.Companies", "Address2", c => c.String());
            AddColumn("dbo.Companies", "Address1", c => c.String());
            DropColumn("dbo.Companies", "Address");
        }
    }
}
