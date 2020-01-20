namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DoctorReports", "Complaints", c => c.String());
            AlterColumn("dbo.DoctorReports", "Complaints2", c => c.String());
            AlterColumn("dbo.DoctorReports", "Analyses", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DoctorReports", "Analyses", c => c.String(maxLength: 500));
            AlterColumn("dbo.DoctorReports", "Complaints2", c => c.String(maxLength: 500));
            AlterColumn("dbo.DoctorReports", "Complaints", c => c.String(maxLength: 500));
        }
    }
}
