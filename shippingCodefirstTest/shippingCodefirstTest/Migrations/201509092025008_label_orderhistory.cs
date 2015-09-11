namespace shippingCodefirstTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class label_orderhistory : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderHistories");
            CreateTable(
                "dbo.Labels",
                c => new
                    {
                        LabelId = c.Int(nullable: false),
                        ver = c.String(nullable: false),
                        lb_content = c.String(storeType: "xml"),
                        state = c.Int(nullable: false),
                        created_time = c.DateTime(nullable: false),
                        last_updated_time = c.DateTime(nullable: false),
                        order_id = c.Int(nullable: false),
                        user_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LabelId)
                .ForeignKey("dbo.OrderHistories", t => t.order_id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.user_id)
                .Index(t => t.order_id)
                .Index(t => t.user_id);
            
            AddColumn("dbo.OrderHistories", "total_sell_price", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.OrderHistories", "total_cost_price", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.OrderHistories", "total_sell_tax", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.OrderHistories", "total_sell_balance", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.OrderHistories", "note", c => c.String());
            AddColumn("dbo.OrderHistories", "state", c => c.Int(nullable: false));
            AddColumn("dbo.OrderHistories", "payment_id", c => c.String());
            AddColumn("dbo.OrderHistories", "created_time", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderHistories", "last_updated_time", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderHistories", "paid_on_time", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderHistories", "user_id", c => c.String(maxLength: 128));
            AlterColumn("dbo.OrderHistories", "OrderId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrderHistories", "OrderId");
            CreateIndex("dbo.OrderHistories", "user_id");
            AddForeignKey("dbo.OrderHistories", "user_id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Labels", "user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderHistories", "user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Labels", "order_id", "dbo.OrderHistories");
            DropIndex("dbo.OrderHistories", new[] { "user_id" });
            DropIndex("dbo.Labels", new[] { "user_id" });
            DropIndex("dbo.Labels", new[] { "order_id" });
            DropPrimaryKey("dbo.OrderHistories");
            AlterColumn("dbo.OrderHistories", "OrderId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.OrderHistories", "user_id");
            DropColumn("dbo.OrderHistories", "paid_on_time");
            DropColumn("dbo.OrderHistories", "last_updated_time");
            DropColumn("dbo.OrderHistories", "created_time");
            DropColumn("dbo.OrderHistories", "payment_id");
            DropColumn("dbo.OrderHistories", "state");
            DropColumn("dbo.OrderHistories", "note");
            DropColumn("dbo.OrderHistories", "total_sell_balance");
            DropColumn("dbo.OrderHistories", "total_sell_tax");
            DropColumn("dbo.OrderHistories", "total_cost_price");
            DropColumn("dbo.OrderHistories", "total_sell_price");
            DropTable("dbo.Labels");
            AddPrimaryKey("dbo.OrderHistories", "OrderId");
        }
    }
}
