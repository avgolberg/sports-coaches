namespace Sports_Coaches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkPlaceAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkPlaces", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkPlaces", "Address");
        }
    }
}
