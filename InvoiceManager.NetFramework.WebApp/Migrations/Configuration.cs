﻿namespace InvoiceManager.NetFramework.WebApp.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<InvoiceManager.NetFramework.WebApp.Models.ApplicationDbContext>
	{
		public Configuration() => AutomaticMigrationsEnabled = false;

		protected override void Seed(InvoiceManager.NetFramework.WebApp.Models.ApplicationDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method
			//  to avoid creating duplicate seed data.
		}
	}
}