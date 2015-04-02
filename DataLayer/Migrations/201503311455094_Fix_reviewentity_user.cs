namespace DRS2Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_reviewentity_user : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ReviewEntities", new[] { "User_UserId" });
            CreateIndex("dbo.ReviewEntities", "user_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReviewEntities", new[] { "user_UserId" });
            CreateIndex("dbo.ReviewEntities", "User_UserId");
        }
    }
}
