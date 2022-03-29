namespace Sports_Coaches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkPlace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkPlaces", "City_Id", c => c.Int());
            CreateIndex("dbo.WorkPlaces", "City_Id");
            AddForeignKey("dbo.WorkPlaces", "City_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkPlaces", "City_Id", "dbo.Cities");
            DropIndex("dbo.WorkPlaces", new[] { "City_Id" });
            DropColumn("dbo.WorkPlaces", "City_Id");
        }
    }
}
