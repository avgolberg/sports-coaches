namespace Sports_Coaches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoachRequiredFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coaches", "Sport_Id", "dbo.Sports");
            DropIndex("dbo.Coaches", new[] { "Sport_Id" });
            AlterColumn("dbo.Coaches", "Sport_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Coaches", "Sport_Id");
            AddForeignKey("dbo.Coaches", "Sport_Id", "dbo.Sports", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coaches", "Sport_Id", "dbo.Sports");
            DropIndex("dbo.Coaches", new[] { "Sport_Id" });
            AlterColumn("dbo.Coaches", "Sport_Id", c => c.Int());
            CreateIndex("dbo.Coaches", "Sport_Id");
            AddForeignKey("dbo.Coaches", "Sport_Id", "dbo.Sports", "Id");
        }
    }
}
