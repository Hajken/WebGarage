namespace WebGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ParkingSpace : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkingSpaces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Edge = c.Boolean(nullable: false),
                        Vehicle_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_ID)
                .Index(t => t.Vehicle_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingSpaces", "Vehicle_ID", "dbo.Vehicles");
            DropIndex("dbo.ParkingSpaces", new[] { "Vehicle_ID" });
            DropTable("dbo.ParkingSpaces");
        }
    }
}
