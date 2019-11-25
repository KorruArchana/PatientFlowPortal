namespace EMIS.PatientFlow.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nugetupdateaspnetidentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PatientFlow.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "PatientFlow.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("PatientFlow.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("PatientFlow.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "PatientFlow.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "PatientFlow.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PatientFlow.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "PatientFlow.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("PatientFlow.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PatientFlow.AspNetUserRoles", "UserId", "PatientFlow.AspNetUsers");
            DropForeignKey("PatientFlow.AspNetUserLogins", "UserId", "PatientFlow.AspNetUsers");
            DropForeignKey("PatientFlow.AspNetUserClaims", "UserId", "PatientFlow.AspNetUsers");
            DropForeignKey("PatientFlow.AspNetUserRoles", "RoleId", "PatientFlow.AspNetRoles");
            DropIndex("PatientFlow.AspNetUserLogins", new[] { "UserId" });
            DropIndex("PatientFlow.AspNetUserClaims", new[] { "UserId" });
            DropIndex("PatientFlow.AspNetUsers", "UserNameIndex");
            DropIndex("PatientFlow.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("PatientFlow.AspNetUserRoles", new[] { "UserId" });
            DropIndex("PatientFlow.AspNetRoles", "RoleNameIndex");
            DropTable("PatientFlow.AspNetUserLogins");
            DropTable("PatientFlow.AspNetUserClaims");
            DropTable("PatientFlow.AspNetUsers");
            DropTable("PatientFlow.AspNetUserRoles");
            DropTable("PatientFlow.AspNetRoles");
        }
    }
}
