using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StudentsDiary.WpfApp.Models.Domains;
using StudentsDiary.WpfApp.Properties;

namespace StudentsDiary.WpfApp.Models;

internal class AppDbContext : DbContext
{
    private static readonly string _connectionString = $@"
		Server={Settings.Default.ServerAddress}\{Settings.Default.ServerName};
		Database={Settings.Default.Database};
		User Id={Settings.Default.User};
		Password={Settings.Default.Password};
		TrustServerCertificate=True;";

    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // INFO - EF Konfiguracja 1 - dodanie wszystkich konfiguracji
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		modelBuilder.Entity<Group>().HasData(
		   new Group { Id = 1, Name = "1A" },
		   new Group { Id = 2, Name = "1B" },
		   new Group { Id = 3, Name = "2A" },
		   new Group { Id = 4, Name = "2B" }
		   );
	}
}