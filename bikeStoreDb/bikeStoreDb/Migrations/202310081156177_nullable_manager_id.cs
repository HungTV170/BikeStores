namespace bikeStoreDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable_manager_id : DbMigration
    {
        public override void Up()
        {
            DropIndex("sales.staffs", new[] { "manager_id" });
            AlterColumn("sales.staffs", "manager_id", c => c.Int());
            CreateIndex("sales.staffs", "manager_id");
        }
        
        public override void Down()
        {
            DropIndex("sales.staffs", new[] { "manager_id" });
            AlterColumn("sales.staffs", "manager_id", c => c.Int(nullable: false));
            CreateIndex("sales.staffs", "manager_id");
        }
    }
}
