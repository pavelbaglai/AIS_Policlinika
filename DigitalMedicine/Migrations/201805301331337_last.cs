namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Institutions", "Address", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Institutions", "Address", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
