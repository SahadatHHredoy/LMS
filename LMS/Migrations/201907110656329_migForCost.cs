namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migForCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Costs", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Costs", "Date");
        }
    }
}
