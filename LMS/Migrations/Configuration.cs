namespace LMS.Migrations
{
    using LMS.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS.Entities.LMSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS.Entities.LMSDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var orgSetting = new OrganizationSetting()
            {
                OrganizationName = "Varasha Unnayan Samity",
                TotalCost =0,
                TotalLoan =0,
                TotalProfit =0,
                TotalSaving =0,
                TotalTransaction =0
            };
            context.OrganizationSettings.AddOrUpdate(s => s.OrganizationName, orgSetting);
            context.SaveChanges();
        }
    }
}
