namespace DRS2Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedausermodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(),
                        LastLoginDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.ReviewEntities", "User_UserId", c => c.Int());
            AddColumn("dbo.Logs", "User_UserId", c => c.Int());
            CreateIndex("dbo.ReviewEntities", "User_UserId");
            CreateIndex("dbo.Logs", "User_UserId");
            AddForeignKey("dbo.Logs", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.ReviewEntities", "User_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewEntities", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Logs", "User_UserId", "dbo.Users");
            DropIndex("dbo.Logs", new[] { "User_UserId" });
            DropIndex("dbo.ReviewEntities", new[] { "User_UserId" });
            DropColumn("dbo.Logs", "User_UserId");
            DropColumn("dbo.ReviewEntities", "User_UserId");
            DropTable("dbo.Users");
        }
    }
}
