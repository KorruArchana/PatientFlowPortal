using System.Data.Entity;
using EMIS.PatientFlow.Services.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EMIS.PatientFlow.Services.Models
{
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Security")
        {
            
		}

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PatientFlow");

            base.OnModelCreating(modelBuilder);
        }
    }
}