namespace shippingCodefirstTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderHistory_nameChange_From_Id_to_OrderId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderHistories","temp",c => c.Int(nullable: false,identity: false));
            DropPrimaryKey("dbo.OrderHistories");
            DropColumn("dbo.OrderHistories","Id");
            AddColumn("dbo.OrderHistories","OrderId",c => c.Int(nullable: false,identity: true));
            AddPrimaryKey("dbo.OrderHistories", "OrderId");
            DropColumn("dbo.OrderHistories","temp");
        }

        public override void Down()
        {
            AddColumn("dbo.OrderHistories","temp",c => c.Int(nullable: false,identity: false));
            DropPrimaryKey("dbo.OrderHistories");
            DropColumn("dbo.OrderHistories","OrderId");
            AddColumn("dbo.OrderHistories", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderHistories", "Id");
            DropColumn("dbo.OrderHistories","temp");
        }
    }
}
