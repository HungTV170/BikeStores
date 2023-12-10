namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cat_bra_pro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "production.brands",
                c => new
                    {
                        brand_id = c.Int(nullable: false, identity: true),
                        brand_name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.brand_id);
            
            CreateTable(
                "production.products",
                c => new
                    {
                        product_id = c.Int(nullable: false, identity: true),
                        product_name = c.String(nullable: false, maxLength: 255),
                        brand_id = c.Int(nullable: false),
                        category_id = c.Int(nullable: false),
                        model_year = c.Short(nullable: false),
                        list_price = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.product_id)
                .ForeignKey("production.brands", t => t.brand_id, cascadeDelete: true)
                .ForeignKey("production.categories", t => t.category_id, cascadeDelete: true)
                .Index(t => t.brand_id)
                .Index(t => t.category_id);
            
            CreateTable(
                "production.categories",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.category_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("production.products", "category_id", "production.categories");
            DropForeignKey("production.products", "brand_id", "production.brands");
            DropIndex("production.products", new[] { "category_id" });
            DropIndex("production.products", new[] { "brand_id" });
            DropTable("production.categories");
            DropTable("production.products");
            DropTable("production.brands");
        }
    }
}
