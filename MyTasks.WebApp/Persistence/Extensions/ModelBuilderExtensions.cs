using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core.Models.Domains;

namespace MyTasks.WebApp.Persistence.Extensions;

public static class ModelBuilderExtensions
{
	public static void SeedCategories(this ModelBuilder builder)
		=> builder.Entity<Category>().HasData(
			new Category { CategoryId = 1, Name = "Ogólne", IsDefault = true },
			new Category { CategoryId = 2, Name = "Praca", IsDefault = true },
			new Category { CategoryId = 3, Name = "Dom", IsDefault = true }
			);
}