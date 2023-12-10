namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order_item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "sales.order_items",
                c => new
                    {
                        order_id = c.Int(nullable: false),
                        item_id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        list_price = c.Decimal(nullable: false, precision: 10, scale: 2),
                        discount = c.Decimal(nullable: false, precision: 4, scale: 2),
                    })
                .PrimaryKey(t => new { t.order_id, t.item_id })
                .ForeignKey("sales.orders", t => t.order_id, cascadeDelete: true)
                .ForeignKey("production.products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.order_id)
                .Index(t => t.product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("sales.order_items", "product_id", "production.products");
            DropForeignKey("sales.order_items", "order_id", "sales.orders");
            DropIndex("sales.order_items", new[] { "product_id" });
            DropIndex("sales.order_items", new[] { "order_id" });
            DropTable("sales.order_items");
        }
    }
}
