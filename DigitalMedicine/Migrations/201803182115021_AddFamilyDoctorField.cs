namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFamilyDoctorField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "IdFamilyDoctor", c => c.Int());
            CreateIndex("dbo.Patients", "IdFamilyDoctor");
            AddForeignKey("dbo.Patients", "IdFamilyDoctor", "dbo.Doctors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "IdFamilyDoctor", "dbo.Doctors");
            DropIndex("dbo.Patients", new[] { "IdFamilyDoctor" });
            DropColumn("dbo.Patients", "IdFamilyDoctor");
        }
    }
}
