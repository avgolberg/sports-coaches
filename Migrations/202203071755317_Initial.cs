namespace Sports_Coaches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.String(),
                        Place = c.String(),
                        Coach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(),
                        PhotoUrl = c.String(),
                        Experience = c.Int(nullable: false),
                        AwayTrainingPrice = c.Int(nullable: false),
                        Rank_Id = c.Int(),
                        Sport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ranks", t => t.Rank_Id)
                .ForeignKey("dbo.Sports", t => t.Sport_Id)
                .Index(t => t.Rank_Id)
                .Index(t => t.Sport_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Diplomas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Coach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Text = c.String(),
                        Positive = c.Boolean(nullable: false),
                        Coach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        StartHour = c.Int(nullable: false),
                        EndHour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Training",
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
            
            CreateTable(
                "dbo.WorkPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoachCertificates",
                c => new
                    {
                        Coach_Id = c.Int(nullable: false),
                        Certificate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Coach_Id, t.Certificate_Id })
                .ForeignKey("dbo.Coaches", t => t.Coach_Id, cascadeDelete: true)
                .ForeignKey("dbo.Certificates", t => t.Certificate_Id, cascadeDelete: true)
                .Index(t => t.Coach_Id)
                .Index(t => t.Certificate_Id);
            
            CreateTable(
                "dbo.CourseCoaches",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        Coach_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.Coach_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id, cascadeDelete: true)
                .Index(t => t.Course_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.DiplomaCoaches",
                c => new
                    {
                        Diploma_Id = c.Int(nullable: false),
                        Coach_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Diploma_Id, t.Coach_Id })
                .ForeignKey("dbo.Diplomas", t => t.Diploma_Id, cascadeDelete: true)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id, cascadeDelete: true)
                .Index(t => t.Diploma_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.LanguageCoaches",
                c => new
                    {
                        Language_Id = c.Int(nullable: false),
                        Coach_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.Coach_Id })
                .ForeignKey("dbo.Languages", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.ScheduleCoaches",
                c => new
                    {
                        Schedule_Id = c.Int(nullable: false),
                        Coach_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Schedule_Id, t.Coach_Id })
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id, cascadeDelete: true)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id, cascadeDelete: true)
                .Index(t => t.Schedule_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.WorkPlaceCoaches",
                c => new
                    {
                        WorkPlace_Id = c.Int(nullable: false),
                        Coach_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkPlace_Id, t.Coach_Id })
                .ForeignKey("dbo.WorkPlaces", t => t.WorkPlace_Id, cascadeDelete: true)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id, cascadeDelete: true)
                .Index(t => t.WorkPlace_Id)
                .Index(t => t.Coach_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkPlaceCoaches", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.WorkPlaceCoaches", "WorkPlace_Id", "dbo.WorkPlaces");
            DropForeignKey("dbo.Training", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.Coaches", "Sport_Id", "dbo.Sports");
            DropForeignKey("dbo.ScheduleCoaches", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.ScheduleCoaches", "Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.Reviews", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.Coaches", "Rank_Id", "dbo.Ranks");
            DropForeignKey("dbo.Phones", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.LanguageCoaches", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.LanguageCoaches", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.DiplomaCoaches", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.DiplomaCoaches", "Diploma_Id", "dbo.Diplomas");
            DropForeignKey("dbo.CourseCoaches", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.CourseCoaches", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CoachCertificates", "Certificate_Id", "dbo.Certificates");
            DropForeignKey("dbo.CoachCertificates", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.Achievements", "Coach_Id", "dbo.Coaches");
            DropIndex("dbo.WorkPlaceCoaches", new[] { "Coach_Id" });
            DropIndex("dbo.WorkPlaceCoaches", new[] { "WorkPlace_Id" });
            DropIndex("dbo.ScheduleCoaches", new[] { "Coach_Id" });
            DropIndex("dbo.ScheduleCoaches", new[] { "Schedule_Id" });
            DropIndex("dbo.LanguageCoaches", new[] { "Coach_Id" });
            DropIndex("dbo.LanguageCoaches", new[] { "Language_Id" });
            DropIndex("dbo.DiplomaCoaches", new[] { "Coach_Id" });
            DropIndex("dbo.DiplomaCoaches", new[] { "Diploma_Id" });
            DropIndex("dbo.CourseCoaches", new[] { "Coach_Id" });
            DropIndex("dbo.CourseCoaches", new[] { "Course_Id" });
            DropIndex("dbo.CoachCertificates", new[] { "Certificate_Id" });
            DropIndex("dbo.CoachCertificates", new[] { "Coach_Id" });
            DropIndex("dbo.Training", new[] { "Coach_Id" });
            DropIndex("dbo.Reviews", new[] { "Coach_Id" });
            DropIndex("dbo.Phones", new[] { "Coach_Id" });
            DropIndex("dbo.Coaches", new[] { "Sport_Id" });
            DropIndex("dbo.Coaches", new[] { "Rank_Id" });
            DropIndex("dbo.Achievements", new[] { "Coach_Id" });
            DropTable("dbo.WorkPlaceCoaches");
            DropTable("dbo.ScheduleCoaches");
            DropTable("dbo.LanguageCoaches");
            DropTable("dbo.DiplomaCoaches");
            DropTable("dbo.CourseCoaches");
            DropTable("dbo.CoachCertificates");
            DropTable("dbo.WorkPlaces");
            DropTable("dbo.Training");
            DropTable("dbo.Sports");
            DropTable("dbo.Schedules");
            DropTable("dbo.Reviews");
            DropTable("dbo.Ranks");
            DropTable("dbo.Phones");
            DropTable("dbo.Languages");
            DropTable("dbo.Diplomas");
            DropTable("dbo.Courses");
            DropTable("dbo.Coaches");
            DropTable("dbo.Certificates");
            DropTable("dbo.Achievements");
        }
    }
}
