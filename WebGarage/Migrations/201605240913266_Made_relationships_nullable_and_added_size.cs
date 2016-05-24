namespace WebGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Made_relationships_nullable_and_added_size : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleTypes", "Size", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VehicleTypes", "Size");
        }
    }
}
