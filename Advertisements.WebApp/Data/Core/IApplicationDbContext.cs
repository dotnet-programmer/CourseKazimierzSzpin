using Advertisements.WebApp.Data.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Advertisements.WebApp.Data.Core;

public interface IApplicationDbContext
{
	DbSet<Address> Addresses { get; set; }
	DbSet<Advertisement> Advertisements { get; set; }
	DbSet<Category> Categories { get; set; }
	DbSet<Subcategory> Subcategories { get; set; }
	int SaveChanges();
}