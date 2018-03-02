namespace AuditTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Gender", c => c.String(maxLength: 2));
            AlterColumn("dbo.People", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Age", c => c.String());
            DropColumn("dbo.People", "Gender");
        }
    }
}
