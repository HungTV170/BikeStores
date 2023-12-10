namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_shipped_date : DbMigration
    {
        public override void Up()
        {
            DropColumn("sales.orders", "shipped_date");
        }
        
        public override void Down()
        {
            AddColumn("sales.orders", "shipped_date", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
