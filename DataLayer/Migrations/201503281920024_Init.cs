namespace DRS2Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        IssueID = c.Int(nullable: false, identity: true),
                        ReviewID = c.Int(nullable: false),
                        IssueIdentity = c.String(),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.IssueID)
                .ForeignKey("dbo.ReviewEntities", t => t.ReviewID, cascadeDelete: true)
                .Index(t => t.ReviewID);
            
            CreateTable(
                "dbo.ReviewEntities",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Responsible = c.String(),
                        ResponsibleEmail = c.String(),
                        NotificationPeriod = c.Int(nullable: false),
                        LastNotified = c.DateTime(nullable: false),
                        IdentityString = c.String(),
                        URL = c.String(),
                        System_DRSSystemID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.DRSSystems", t => t.System_DRSSystemID)
                .Index(t => t.System_DRSSystemID);
            
            CreateTable(
                "dbo.DRSSystems",
                c => new
                    {
                        DRSSystemID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.DRSSystemID);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Severity = c.Int(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.LogID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewEntities", "System_DRSSystemID", "dbo.DRSSystems");
            DropForeignKey("dbo.Issues", "ReviewID", "dbo.ReviewEntities");
            DropIndex("dbo.ReviewEntities", new[] { "System_DRSSystemID" });
            DropIndex("dbo.Issues", new[] { "ReviewID" });
            DropTable("dbo.Logs");
            DropTable("dbo.DRSSystems");
            DropTable("dbo.ReviewEntities");
            DropTable("dbo.Issues");
        }
    }
}
