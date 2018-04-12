namespace TCSOffice.Business.DataAccess.Migrations
{
    using Domain.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TCSOffice.Business.DataAccess.TCSOfficeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TCSOffice.Business.DataAccess.TCSOfficeDbContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Companies.AddOrUpdate(
                p => p.CompanyName,
                new Company
                {
                    CompanyName = "Harish Company",
                    Address = "H no 64 Cameroon",
                    Email = "yadavharish004@hotmail.com",
                    DateCreated = DateTime.UtcNow,
                    IsActive = true,
                    Phone = "9876543210"
                });

            context.SaveChanges();

            context.Logins.AddOrUpdate(
                p => p.UserName,
              new Login
              {
                  UserName = "harish",
                  Password = "Test@123",
                  IsActive = true,
                  DateCreated = DateTime.UtcNow,
                  Email = "yadavharish004@hotmail.com",
                  IsAdmin = true,
                  Company = context.Companies.FirstOrDefault(z=>z.CompanyName == "Harish Company")
              });
            context.SaveChanges();
        }
    }
}
