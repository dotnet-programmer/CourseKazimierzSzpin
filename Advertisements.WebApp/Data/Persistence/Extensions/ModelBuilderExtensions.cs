using Advertisements.WebApp.Data.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Advertisements.WebApp.Data.Persistence.Extensions;

public static class ModelBuilderExtensions
{
	public static void SeedCategories(this ModelBuilder builder)
	{
		string[] categories = { "Motoryzacja", "Praca", "Dom i Ogród", "Nieruchomości", "Elektronika", "Moda", "Zwierzęta", "Sport i Hobby", "Zdrowie i Uroda", "Dziecko" };

		for (int i = 0; i < categories.Length; i++)
		{
			builder.Entity<Category>().HasData(new Category { CategoryId = i + 1, Name = categories[i] });
		}
	}

	//public static void SeedCategories(this ModelBuilder builder)
	//	=> builder.Entity<Category>().HasData(
	//		new Category { CategoryId = 1, Name = "Motoryzacja" },
	//		new Category { CategoryId = 2, Name = "Praca" },
	//		new Category { CategoryId = 3, Name = "Dom i Ogród" },
	//		new Category { CategoryId = 4, Name = "Nieruchomości" },
	//		new Category { CategoryId = 5, Name = "Elektronika" },
	//		new Category { CategoryId = 6, Name = "Moda" },
	//		new Category { CategoryId = 7, Name = "Zwierzęta" },
	//		new Category { CategoryId = 8, Name = "Sport i Hobby" },
	//		new Category { CategoryId = 9, Name = "Zdrowie i Uroda" },
	//		new Category { CategoryId = 10, Name = "Dziecko" }
	//		);
}