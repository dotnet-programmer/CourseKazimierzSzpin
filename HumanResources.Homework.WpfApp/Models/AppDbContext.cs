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
		UserSettings userSettings = new();
		string connectionString = $"Server={userSettings.ServerAddress}\\{userSettings.ServerName};"
			+ "Database={userSettings.Database};"
			+ "User Id={userSettings.User};"
			+ "Password={userSettings.Password};"
			+ "TrustServerCertificate=True;";

		optionsBuilder.UseSqlServer(connectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		modelBuilder.SeedData();
	}
}