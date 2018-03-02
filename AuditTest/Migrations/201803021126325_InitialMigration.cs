namespace AuditTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditTrails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KeyField = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        DataModel = c.String(),
                        ValueBefore = c.String(),
                        ValueAfter = c.String(),
                        Changes = c.String(),
                        AuditAction = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 35),
                        LastName = c.String(maxLength: 35),
                        Age = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.People");
            DropTable("dbo.AuditTrails");
        }
    }
}
