namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "sales.staffs",
                c => new
                    {
                        staff_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(nullable: false, maxLength: 50),
                        last_name = c.String(nullable: false, maxLength: 50),
                        email = c.String(nullable: false, maxLength: 255),
                        phone = c.String(maxLength: 25),
                        active = c.Byte(nullable: false),
                        store_id = c.Int(nullable: false),
                        manager_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.staff_id)
                .ForeignKey("sales.staffs", t => t.manager_id)
                .ForeignKey("sales.stores", t => t.store_id, cascadeDelete: true)
                .Index(t => t.email, unique: true)
                .Index(t => t.store_id)
                .Index(t => t.manager_id);
            
            CreateTable(
                "sales.stores",
                c => new
                    {
                        store_id = c.Int(nullable: false, identity: true),
                        store_name = c.String(nullable: false, maxLength: 255),
                        phone = c.String(maxLength: 25),
                        email = c.String(maxLength: 255),
                        street = c.String(maxLength: 255),
                        city = c.String(maxLength: 255),
                        state = c.String(maxLength: 10),
                        zip_code = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.store_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("sales.staffs", "store_id", "sales.stores");
            DropForeignKey("sales.staffs", "manager_id", "sales.staffs");
            DropIndex("sales.staffs", new[] { "manager_id" });
            DropIndex("sales.staffs", new[] { "store_id" });
            DropIndex("sales.staffs", new[] { "email" });
            DropTable("sales.stores");
            DropTable("sales.staffs");
        }
    }
}
