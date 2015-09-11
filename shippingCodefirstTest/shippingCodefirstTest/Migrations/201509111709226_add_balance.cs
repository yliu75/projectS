namespace shippingCodefirstTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_balance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "balance", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "balance");
        }
    }
}
