namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReferenceWithAnalysis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LaboratoryReports", "IdLaboratoryAnalysis", c => c.Int());
            CreateIndex("dbo.LaboratoryReports", "IdLaboratoryAnalysis");
            AddForeignKey("dbo.LaboratoryReports", "IdLaboratoryAnalysis", "dbo.LaboratoryAnalysis", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LaboratoryReports", "IdLaboratoryAnalysis", "dbo.LaboratoryAnalysis");
            DropIndex("dbo.LaboratoryReports", new[] { "IdLaboratoryAnalysis" });
            DropColumn("dbo.LaboratoryReports", "IdLaboratoryAnalysis");
        }
    }
}
