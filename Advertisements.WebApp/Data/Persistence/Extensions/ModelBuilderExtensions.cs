using Advertisements.WebApp.Data.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Advertisements.WebApp.Data.Persistence.Extensions;

public static class ModelBuilderExtensions
{
	public static void SeedCategories(this ModelBuilder builder)
	{
		string[] categoryNames = ["Motoryzacja", "Praca", "Dom i Ogród", "Nieruchomości", "Elektronika", "Moda", "Zwierzęta", "Sport i Hobby", "Zdrowie i Uroda", "Dziecko"];

		List<Category> categories = [];
		for (int i = 0; i < categoryNames.Length; i++)
		{
			categories.Add(new() { CategoryId = i + 1, Name = categoryNames[i] });
		}
		builder.Entity<Category>().HasData(categories);
	}
}