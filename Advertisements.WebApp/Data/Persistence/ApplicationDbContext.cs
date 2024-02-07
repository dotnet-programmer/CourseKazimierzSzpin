using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Advertisements.WebApp.Data.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

	public DbSet<Address> Addresses { get; set; }
	public DbSet<Advertisement> Advertisements { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Subcategory> Subcategories { get; set; }
}