namespace DigitalMedicine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeForeignKeyForComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "IdUser", "dbo.Patients");
            AddForeignKey("dbo.Comments", "IdUser", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "IdUser", "dbo.Users");
            AddForeignKey("dbo.Comments", "IdUser", "dbo.Patients", "Id");
        }
    }
}
