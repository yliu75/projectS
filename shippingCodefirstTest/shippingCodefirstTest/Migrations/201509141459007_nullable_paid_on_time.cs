namespace shippingCodefirstTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable_paid_on_time : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderHistories", "paid_on_time", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderHistories", "paid_on_time", c => c.DateTime(nullable: false));
        }
    }
}
