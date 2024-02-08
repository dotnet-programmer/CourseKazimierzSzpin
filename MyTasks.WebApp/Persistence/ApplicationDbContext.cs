using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Persistence.Extensions;

namespace MyTasks.WebApp.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<Core.Models.Domains.Task> Tasks { get; set; }
	public DbSet<Category> Categories { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<ApplicationUser>()
			.HasMany(x => x.Categories)
			.WithOne(x => x.User)
			.OnDelete(DeleteBehavior.Restrict);

		builder.SeedCategories();

		base.OnModelCreating(builder);
	}
}