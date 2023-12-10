namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cus_ord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "sales.customers",
                c => new
                    {
                        customer_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(nullable: false, maxLength: 255),
                        last_name = c.String(nullable: false, maxLength: 255),
                        phone = c.String(maxLength: 25),
                        email = c.String(nullable: false, maxLength: 255),
                        street = c.String(maxLength: 255),
                        city = c.String(maxLength: 50),
                        state = c.String(maxLength: 25),
                        zip_code = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.customer_id);
            
            CreateTable(
                "sales.orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
                        customer_id = c.Int(nullable: false),
                        order_date = c.DateTime(nullable: false),
                        required_date = c.DateTime(nullable: false),
                        shipped_date = c.DateTime(nullable: false),
                        store_id = c.Int(nullable: false),
                        staff_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("sales.customers", t => t.customer_id, cascadeDelete: true)
                .ForeignKey("sales.staffs", t => t.staff_id)
                .ForeignKey("sales.stores", t => t.store_id, cascadeDelete: true)
                .Index(t => t.customer_id)
                .Index(t => t.store_id)
                .Index(t => t.staff_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("sales.orders", "store_id", "sales.stores");
            DropForeignKey("sales.orders", "staff_id", "sales.staffs");
            DropForeignKey("sales.orders", "customer_id", "sales.customers");
            DropIndex("sales.orders", new[] { "staff_id" });
            DropIndex("sales.orders", new[] { "store_id" });
            DropIndex("sales.orders", new[] { "customer_id" });
            DropTable("sales.orders");
            DropTable("sales.customers");
        }
    }
}
