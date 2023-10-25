namespace SendEmail.WebApp.NetFramework.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<SendEmail.WebApp.NetFramework.Models.AppDbContext>
	{
		public Configuration() => AutomaticMigrationsEnabled = false;

		protected override void Seed(SendEmail.WebApp.NetFramework.Models.AppDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method
			//  to avoid creating duplicate seed data.
		}
	}
}