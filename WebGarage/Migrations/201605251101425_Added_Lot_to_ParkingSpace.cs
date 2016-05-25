namespace WebGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Lot_to_ParkingSpace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkingSpaces", "Lot", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParkingSpaces", "Lot");
        }
    }
}
