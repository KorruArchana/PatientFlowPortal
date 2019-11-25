namespace EMIS.PatientFlow.Services.Migrations
{
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EMIS.PatientFlow.Services.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EMIS.PatientFlow.Services.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            InitializeUser();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
        }

        private static void InitializeUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!roleManager.RoleExists("Practice Admin"))
                roleManager.Create(new IdentityRole("Practice Admin"));

            if (!roleManager.RoleExists("EMIS Super User"))
                roleManager.Create(new IdentityRole("EMIS Super User"));

            var user = userManager.FindByName("Administrator");

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "Administrator",
                    Email = "Administrator@qburst.com"
                };

                var result = userManager.Create(
                    user,
                    "A1d2m3i4n#123");
                if (result.Succeeded)
                {
                    userManager.AddToRole(
                        user.Id,
                        "EMIS Super User");
                }
            }
        }
    }
}
