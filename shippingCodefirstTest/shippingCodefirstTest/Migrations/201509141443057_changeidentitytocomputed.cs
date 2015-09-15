namespace shippingCodefirstTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeidentitytocomputed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Labels", "order_id", "dbo.OrderHistories");
            DropPrimaryKey("dbo.OrderHistories");
            AlterColumn("dbo.OrderHistories", "OrderId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.OrderHistories", "created_time", c => c.DateTime(nullable: false,defaultValueSql:"GETUTCDATE()"));
            AlterColumn("dbo.OrderHistories", "last_updated_time", c => c.DateTime(nullable: false,defaultValueSql: "GETUTCDATE()"));
            AddPrimaryKey("dbo.OrderHistories", "OrderId");
            AddForeignKey("dbo.Labels", "order_id", "dbo.OrderHistories", "OrderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Labels", "order_id", "dbo.OrderHistories");
            DropPrimaryKey("dbo.OrderHistories");
            AlterColumn("dbo.OrderHistories", "last_updated_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrderHistories", "created_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrderHistories", "OrderId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrderHistories", "OrderId");
            AddForeignKey("dbo.Labels", "order_id", "dbo.OrderHistories", "OrderId", cascadeDelete: true);
        }
    }
}
