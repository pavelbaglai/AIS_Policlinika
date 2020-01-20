namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceptionTimeForAnalysis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LaboratoryAnalysis", "ReceptionTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LaboratoryAnalysis", "ReceptionTime");
        }
    }
}
