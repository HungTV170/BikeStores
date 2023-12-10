namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateTime_To_Date : DbMigration
    {
        public override void Up()
        {
            AlterColumn("sales.orders", "order_date", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("sales.orders", "required_date", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("sales.orders", "shipped_date", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("sales.orders", "shipped_date", c => c.DateTime(nullable: false));
            AlterColumn("sales.orders", "required_date", c => c.DateTime(nullable: false));
            AlterColumn("sales.orders", "order_date", c => c.DateTime(nullable: false));
        }
    }
}
