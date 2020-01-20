namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsInMedCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorReports", "Temperature", c => c.String(maxLength: 10));
            AddColumn("dbo.DoctorReports", "Complaints", c => c.String(maxLength: 500));
            AddColumn("dbo.DoctorReports", "Complaints2", c => c.String(maxLength: 500));
            AddColumn("dbo.DoctorReports", "Analyses", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DoctorReports", "Analyses");
            DropColumn("dbo.DoctorReports", "Complaints2");
            DropColumn("dbo.DoctorReports", "Complaints");
            DropColumn("dbo.DoctorReports", "Temperature");
        }
    }
}
