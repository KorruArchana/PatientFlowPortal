namespace EMIS.PatientFlow.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name : DbMigration
    {
        public override void Up()
        {
			AddColumn("PatientFlow.AspNetUsers", "Name", c => c.String());
			Sql("UPDATE PatientFlow.AspNetUsers SET Name=UserName");
		}
        
        public override void Down()
        {
			DropColumn("PatientFlow.AspNetUsers", "Name");
		}
    }
}
