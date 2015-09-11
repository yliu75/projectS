namespace shippingCodefirstTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class state_index : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Labels", "state");
            CreateIndex("dbo.OrderHistories", "state");
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderHistories", new[] { "state" });
            DropIndex("dbo.Labels", new[] { "state" });
        }
    }
}
