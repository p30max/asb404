namespace Asb404.Migrations
{
    using Asb404.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Asb404.Models.DBContexter>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Asb404.Models.DBContexter context)
        {
            if (!context.Groups.Any())
            {
                context.Groups.AddOrUpdate(x => x.Id,
                    new Group() { Name = "طرح نظارتی" },
                    new Group() { Name = "اجرای احکام" });
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                context.Users.AddOrUpdate(x => x.UserName, new Users()
                {
                    NameFamily  = "Administrator",
                    UserName    = "admin",
                    Password    = Asb404.Models.Tools.HashPassword("123456"),
                    Email       = "admin@example.com",
                    Mobail      = "09120000000",
                    Role        = "Admin",
                    isApproved  = true,
                    Ostan       = 1,
                    Shahrestan  = "Tehran",
                    Address     = "Default Address",
                    PostCode    = "1111111111"
                });
            }
        }
    }
}
