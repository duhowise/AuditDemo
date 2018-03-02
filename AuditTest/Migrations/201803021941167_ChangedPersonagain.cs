namespace AuditTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPersonagain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Deleted");
        }
    }
}
