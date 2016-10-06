namespace FirstAPI.Migrations
{
    using FirstAPI.AppEnum;
    using FirstAPI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FirstAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FirstAPI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Items.AddOrUpdate(
                i => i.Id,
                new Items { Id = 1, Title = "Item 1", Description = "Ouvir o despertador", Date = new DateTime(2016, 09, 17, 7, 5, 0), Status = StatusEnum.COMPLETED, Hours = 7, Minute = 5},
                new Items { Id = 1, Title = "Item 2", Description = "Abrir os olhinhos", Date = new DateTime(2016, 09, 17, 7, 5, 3), Status = StatusEnum.INPROGRESS, Hours = 7, Minute = 6},
                new Items { Id = 1, Title = "Item 3", Description = "Me levantar", Date = new DateTime(2016, 09, 17, 7, 25, 0), Status = StatusEnum.TODO, Hours = 8, Minute = 8}
            );
        }
    }
}
