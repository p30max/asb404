﻿namespace Asb404.Migrations
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
            if(!context.Groups.Any())
            { 
            context.Groups.AddOrUpdate(x => x.Id, new Group() { Name = "طرح نظارتی" },new Group() { Name="اجرای احکام"});
                //  This method will be called after migrating to the latest version.

                //  You can use the DbSet<T>.AddOrUpdate() helper extension method
                //  to avoid creating duplicate seed data.
            }
        }
    }
}
