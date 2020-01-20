namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNullValue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "IdRole", "dbo.Roles");
            DropForeignKey("dbo.Comments", "IdNews", "dbo.News");
            DropForeignKey("dbo.DoctorReports", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", "dbo.LaboratoryAnalysis");
            DropForeignKey("dbo.Symptoms", "IdSymptomGroup", "dbo.SymptomGroups");
            DropForeignKey("dbo.SymptomWeights", "IdDiagnosis", "dbo.Diagnosis");
            DropForeignKey("dbo.SymptomWeights", "IdSymptom", "dbo.Symptoms");
            DropIndex("dbo.Appointments", new[] { "IdDoctor" });
            DropIndex("dbo.Appointments", new[] { "IdPatient" });
            DropIndex("dbo.Users", new[] { "IdRole" });
            DropIndex("dbo.Comments", new[] { "IdUser" });
            DropIndex("dbo.Comments", new[] { "IdNews" });
            DropIndex("dbo.DoctorReports", new[] { "DiagnosisId" });
            DropIndex("dbo.DoctorReports", new[] { "DoctorId" });
            DropIndex("dbo.DoctorReports", new[] { "PatientId" });
            DropIndex("dbo.LaboratoryReports", new[] { "LabaratoryrId" });
            DropIndex("dbo.LaboratoryAppointments", new[] { "IdLaboratoryAnalysis" });
            DropIndex("dbo.LaboratoryAppointments", new[] { "IdPatient" });
            DropIndex("dbo.Symptoms", new[] { "IdSymptomGroup" });
            DropIndex("dbo.SymptomWeights", new[] { "IdSymptom" });
            DropIndex("dbo.SymptomWeights", new[] { "IdDiagnosis" });
            DropIndex("dbo.ClinicNews", new[] { "IdClinic" });
            RenameColumn(table: "dbo.LaboratoryReports", name: "LabaratoryrId", newName: "LabaratoryId");
            AlterColumn("dbo.Appointments", "IdDoctor", c => c.Int(nullable: false));
            AlterColumn("dbo.Appointments", "IdPatient", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "IdRole", c => c.Int(nullable: false));
            AlterColumn("dbo.ClinicNews", "IdClinic", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "IdUser", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "IdNews", c => c.Int(nullable: false));
            AlterColumn("dbo.DoctorReports", "DiagnosisId", c => c.Int(nullable: false));
            AlterColumn("dbo.DoctorReports", "DoctorId", c => c.Int(nullable: false));
            AlterColumn("dbo.DoctorReports", "PatientId", c => c.Int(nullable: false));
            AlterColumn("dbo.LaboratoryReports", "LabaratoryId", c => c.Int());
            AlterColumn("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", c => c.Int(nullable: false));
            AlterColumn("dbo.LaboratoryAppointments", "IdPatient", c => c.Int(nullable: false));
            AlterColumn("dbo.Symptoms", "IdSymptomGroup", c => c.Int(nullable: false));
            AlterColumn("dbo.SymptomWeights", "IdSymptom", c => c.Int(nullable: false));
            AlterColumn("dbo.SymptomWeights", "IdDiagnosis", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "IdDoctor");
            CreateIndex("dbo.Appointments", "IdPatient");
            CreateIndex("dbo.Users", "IdRole");
            CreateIndex("dbo.Comments", "IdUser");
            CreateIndex("dbo.Comments", "IdNews");
            CreateIndex("dbo.DoctorReports", "DiagnosisId");
            CreateIndex("dbo.DoctorReports", "DoctorId");
            CreateIndex("dbo.DoctorReports", "PatientId");
            CreateIndex("dbo.LaboratoryReports", "LabaratoryId");
            CreateIndex("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis");
            CreateIndex("dbo.LaboratoryAppointments", "IdPatient");
            CreateIndex("dbo.Symptoms", "IdSymptomGroup");
            CreateIndex("dbo.SymptomWeights", "IdSymptom");
            CreateIndex("dbo.SymptomWeights", "IdDiagnosis");
            CreateIndex("dbo.ClinicNews", "IdClinic");
            AddForeignKey("dbo.Users", "IdRole", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "IdNews", "dbo.News", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DoctorReports", "DiagnosisId", "dbo.Diagnosis", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", "dbo.LaboratoryAnalysis", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Symptoms", "IdSymptomGroup", "dbo.SymptomGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SymptomWeights", "IdDiagnosis", "dbo.Diagnosis", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SymptomWeights", "IdSymptom", "dbo.Symptoms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SymptomWeights", "IdSymptom", "dbo.Symptoms");
            DropForeignKey("dbo.SymptomWeights", "IdDiagnosis", "dbo.Diagnosis");
            DropForeignKey("dbo.Symptoms", "IdSymptomGroup", "dbo.SymptomGroups");
            DropForeignKey("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", "dbo.LaboratoryAnalysis");
            DropForeignKey("dbo.DoctorReports", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.Comments", "IdNews", "dbo.News");
            DropForeignKey("dbo.Users", "IdRole", "dbo.Roles");
            DropIndex("dbo.ClinicNews", new[] { "IdClinic" });
            DropIndex("dbo.SymptomWeights", new[] { "IdDiagnosis" });
            DropIndex("dbo.SymptomWeights", new[] { "IdSymptom" });
            DropIndex("dbo.Symptoms", new[] { "IdSymptomGroup" });
            DropIndex("dbo.LaboratoryAppointments", new[] { "IdPatient" });
            DropIndex("dbo.LaboratoryAppointments", new[] { "IdLaboratoryAnalysis" });
            DropIndex("dbo.LaboratoryReports", new[] { "LabaratoryId" });
            DropIndex("dbo.DoctorReports", new[] { "PatientId" });
            DropIndex("dbo.DoctorReports", new[] { "DoctorId" });
            DropIndex("dbo.DoctorReports", new[] { "DiagnosisId" });
            DropIndex("dbo.Comments", new[] { "IdNews" });
            DropIndex("dbo.Comments", new[] { "IdUser" });
            DropIndex("dbo.Users", new[] { "IdRole" });
            DropIndex("dbo.Appointments", new[] { "IdPatient" });
            DropIndex("dbo.Appointments", new[] { "IdDoctor" });
            AlterColumn("dbo.SymptomWeights", "IdDiagnosis", c => c.Int());
            AlterColumn("dbo.SymptomWeights", "IdSymptom", c => c.Int());
            AlterColumn("dbo.Symptoms", "IdSymptomGroup", c => c.Int());
            AlterColumn("dbo.LaboratoryAppointments", "IdPatient", c => c.Int());
            AlterColumn("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", c => c.Int());
            AlterColumn("dbo.LaboratoryReports", "LabaratoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.DoctorReports", "PatientId", c => c.Int());
            AlterColumn("dbo.DoctorReports", "DoctorId", c => c.Int());
            AlterColumn("dbo.DoctorReports", "DiagnosisId", c => c.Int());
            AlterColumn("dbo.Comments", "IdNews", c => c.Int());
            AlterColumn("dbo.Comments", "IdUser", c => c.Int());
            AlterColumn("dbo.ClinicNews", "IdClinic", c => c.Int());
            AlterColumn("dbo.Users", "IdRole", c => c.Int());
            AlterColumn("dbo.Appointments", "IdPatient", c => c.Int());
            AlterColumn("dbo.Appointments", "IdDoctor", c => c.Int());
            RenameColumn(table: "dbo.LaboratoryReports", name: "LabaratoryId", newName: "LabaratoryrId");
            CreateIndex("dbo.ClinicNews", "IdClinic");
            CreateIndex("dbo.SymptomWeights", "IdDiagnosis");
            CreateIndex("dbo.SymptomWeights", "IdSymptom");
            CreateIndex("dbo.Symptoms", "IdSymptomGroup");
            CreateIndex("dbo.LaboratoryAppointments", "IdPatient");
            CreateIndex("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis");
            CreateIndex("dbo.LaboratoryReports", "LabaratoryrId");
            CreateIndex("dbo.DoctorReports", "PatientId");
            CreateIndex("dbo.DoctorReports", "DoctorId");
            CreateIndex("dbo.DoctorReports", "DiagnosisId");
            CreateIndex("dbo.Comments", "IdNews");
            CreateIndex("dbo.Comments", "IdUser");
            CreateIndex("dbo.Users", "IdRole");
            CreateIndex("dbo.Appointments", "IdPatient");
            CreateIndex("dbo.Appointments", "IdDoctor");
            AddForeignKey("dbo.SymptomWeights", "IdSymptom", "dbo.Symptoms", "Id");
            AddForeignKey("dbo.SymptomWeights", "IdDiagnosis", "dbo.Diagnosis", "Id");
            AddForeignKey("dbo.Symptoms", "IdSymptomGroup", "dbo.SymptomGroups", "Id");
            AddForeignKey("dbo.LaboratoryAppointments", "IdLaboratoryAnalysis", "dbo.LaboratoryAnalysis", "Id");
            AddForeignKey("dbo.DoctorReports", "DiagnosisId", "dbo.Diagnosis", "Id");
            AddForeignKey("dbo.Comments", "IdNews", "dbo.News", "Id");
            AddForeignKey("dbo.Users", "IdRole", "dbo.Roles", "Id");
        }
    }
}
