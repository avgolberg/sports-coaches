namespace Sports_Coaches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoachUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AwayTraining",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Time = c.String(),
                        Price = c.Int(nullable: false),
                        Coach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id)
                .Index(t => t.Coach_Id);
            
            DropColumn("dbo.Coaches", "AwayTrainingPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coaches", "AwayTrainingPrice", c => c.Int(nullable: false));
            DropForeignKey("dbo.AwayTraining", "Coach_Id", "dbo.Coaches");
            DropIndex("dbo.AwayTraining", new[] { "Coach_Id" });
            DropTable("dbo.AwayTraining");
        }
    }
}
