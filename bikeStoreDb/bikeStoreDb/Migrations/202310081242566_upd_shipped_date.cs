namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd_shipped_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("sales.orders", "shipped_date", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("sales.orders", "shipped_date");
        }
    }
}
