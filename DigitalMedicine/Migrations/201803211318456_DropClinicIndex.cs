namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropClinicIndex : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OtherWorkers", "Clinic_Id", "dbo.Clinics");
            DropIndex("dbo.OtherWorkers", new[] { "Clinic_Id" });
            DropColumn("dbo.OtherWorkers", "Clinic_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OtherWorkers", "Clinic_Id", c => c.Int());
            CreateIndex("dbo.OtherWorkers", "Clinic_Id");
            AddForeignKey("dbo.OtherWorkers", "Clinic_Id", "dbo.Clinics", "Id");
        }
    }
}
