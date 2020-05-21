namespace ContactWebDNF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateContactTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 40),
                        Phone = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Address1 = c.String(nullable: false, maxLength: 250),
                        Address2 = c.String(nullable: false, maxLength: 250),
                        City = c.String(nullable: false, maxLength: 50),
                        Postcode = c.String(nullable: false, maxLength: 10),
                        StateId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.StateId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Abbreviation = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contacts", "StateId", "dbo.States");
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropIndex("dbo.Contacts", new[] { "StateId" });
            DropTable("dbo.States");
            DropTable("dbo.Contacts");
        }
    }
}
