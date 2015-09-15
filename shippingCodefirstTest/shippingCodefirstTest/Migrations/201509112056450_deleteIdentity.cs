namespace shippingCodefirstTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteIdentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Labels", "created_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Labels", "last_updated_time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Labels", "last_updated_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Labels", "created_time", c => c.DateTime(nullable: false));
        }
    }
}
