namespace WebGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_relationships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Member_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "VehicleType_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "Member_ID");
            CreateIndex("dbo.Vehicles", "VehicleType_ID");
            AddForeignKey("dbo.Vehicles", "Member_ID", "dbo.Members", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Vehicles", "VehicleType_ID", "dbo.VehicleTypes", "ID", cascadeDelete: true);
            DropColumn("dbo.Vehicles", "VehicleType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "VehicleType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Vehicles", "VehicleType_ID", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "Member_ID", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "VehicleType_ID" });
            DropIndex("dbo.Vehicles", new[] { "Member_ID" });
            DropColumn("dbo.Vehicles", "VehicleType_ID");
            DropColumn("dbo.Vehicles", "Member_ID");
        }
    }
}
