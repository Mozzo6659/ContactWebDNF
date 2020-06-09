namespace ContactWebDNF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chngeaddress2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "City", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "City", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
