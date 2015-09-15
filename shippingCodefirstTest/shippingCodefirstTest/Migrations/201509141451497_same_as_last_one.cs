namespace shippingCodefirstTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class same_as_last_one : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Labels", "created_time", c => c.DateTime(nullable: false,defaultValueSql: "GETUTCDATE()"));
            AlterColumn("dbo.Labels", "last_updated_time", c => c.DateTime(nullable: false,defaultValueSql: "GETUTCDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Labels", "last_updated_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Labels", "created_time", c => c.DateTime(nullable: false));
        }
    }
}
