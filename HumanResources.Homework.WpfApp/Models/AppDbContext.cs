using System.Reflection;
using HumanResources.Homework.WpfApp.Models.Domains;
using HumanResources.Homework.WpfApp.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Homework.WpfApp.Models;

internal class AppDbContext : DbContext
{
	public DbSet<Employee> Employees { get; set; }
	public DbSet<WorkTime> WorkTimes { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Log> Logs { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		string connectionString = $@"
			Server={UserSettings.GetStringFromConfig(nameof(UserSettings.ServerAddress))}\{UserSettings.GetStringFromConfig(nameof(UserSettings.ServerName))};
			Database={UserSettings.GetStringFromConfig(nameof(UserSettings.Database))};
			User Id={UserSettings.GetStringFromConfig(nameof(UserSettings.User))};
			Password={StringCipherHelper.DecryptStringFromConfig(nameof(UserSettings.Password))};
			TrustServerCertificate=True;
		";

		optionsBuilder.UseSqlServer(connectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		modelBuilder.SeedUsers();
		modelBuilder.SeedWorkTimes();
	}
}