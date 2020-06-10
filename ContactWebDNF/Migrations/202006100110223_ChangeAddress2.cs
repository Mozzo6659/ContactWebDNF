namespace ContactWebDNF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAddress2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Address2", c => c.String(maxLength: 250));
            AlterColumn("dbo.Contacts", "City", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.Contacts", "Address2", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
