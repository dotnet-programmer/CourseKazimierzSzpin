using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models.Domains;

namespace MyTasks.WebApp.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
	public DbSet<Core.Models.Domains.Task> Tasks { get; set; }
	public DbSet<Category> Categories { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<ApplicationUser>()
			.HasMany(x => x.Categories)
			.WithOne(x => x.User)
			.OnDelete(DeleteBehavior.Restrict);

		base.OnModelCreating(builder);
	}
}