namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201803222023140_AddLAboratoryAppointments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LaboratoryAppointments", "IdLabaratory", "dbo.Labaratories");
            DropIndex("dbo.LaboratoryAppointments", new[] { "IdLabaratory" });
            AddColumn("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", c => c.Int());
            CreateIndex("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis");
            AddForeignKey("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", "dbo.LaboratoryAnalysis", "Id");
            DropColumn("dbo.LaboratoryAppointments", "IdLabaratory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LaboratoryAppointments", "IdLabaratory", c => c.Int());
            DropForeignKey("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", "dbo.LaboratoryAnalysis");
            DropIndex("dbo.LaboratoryAppointments", new[] { "IdLaboratoryAnalysis" });
            DropColumn("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis");
            CreateIndex("dbo.LaboratoryAppointments", "IdLabaratory");
            AddForeignKey("dbo.LaboratoryAppointments", "IdLabaratory", "dbo.Labaratories", "Id");
        }
    }
}
