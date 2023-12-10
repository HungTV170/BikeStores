namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_order_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("sales.orders", "order_status", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("sales.orders", "order_status");
        }
    }
}
