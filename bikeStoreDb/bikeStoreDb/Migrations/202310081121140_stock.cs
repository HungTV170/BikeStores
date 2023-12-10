namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "production.stocks",
                c => new
                    {
                        product_id = c.Int(nullable: false),
                        store_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.product_id, t.store_id })
                .ForeignKey("production.products", t => t.product_id, cascadeDelete: true)
                .ForeignKey("sales.stores", t => t.store_id, cascadeDelete: true)
                .Index(t => t.product_id)
                .Index(t => t.store_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("production.stocks", "store_id", "sales.stores");
            DropForeignKey("production.stocks", "product_id", "production.products");
            DropIndex("production.stocks", new[] { "store_id" });
            DropIndex("production.stocks", new[] { "product_id" });
            DropTable("production.stocks");
        }
    }
}
