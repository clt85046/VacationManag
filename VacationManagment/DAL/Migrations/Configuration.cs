namespace DAL.Migrations
{
	using DAL.Model;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.MainContext context)
        {
			context.Roles.AddOrUpdate(new Role {Name="HR"}, new Role { Name = "User" }, new Role { Name = "Manager" }, new Role { Name = "Employee" });
			context.Policies.AddOrUpdate(new Policy { MinYearsOfOffice = 0, MaxYearsOfOffice = 15, PaidDayOffs = 5, PaidSickness = 5, UnPaidDayOffs = 5, UnPaidSickness = 5 });
			context.Users.AddOrUpdate(new User { Email = "admin@admin.com", FirstName = "Viacheslav", LastName = "Tarbinskyi", YearsOfService = 10 });
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
		}
    }
}
