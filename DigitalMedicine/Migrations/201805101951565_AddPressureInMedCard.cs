namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPressureInMedCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorReports", "Pressure", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DoctorReports", "Pressure");
        }
    }
}
