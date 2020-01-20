namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Institutions", "Name", c => c.String());
            AlterColumn("dbo.Institutions", "About", c => c.String());
            AlterColumn("dbo.Institutions", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Institutions", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Institutions", "Phone", c => c.String(maxLength: 17));
            AlterColumn("dbo.Institutions", "Address", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Institutions", "About", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Institutions", "Name", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
