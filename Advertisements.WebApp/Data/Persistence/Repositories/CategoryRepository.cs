using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Repositories;

namespace Advertisements.WebApp.Data.Persistence.Repositories;

public class CategoryRepository(IApplicationDbContext context) : ICategoryRepository
{
	public IEnumerable<Category> GetCategories()
		=> context.Categories
			.OrderBy(x => x.Name)
			.ToList();

	public IEnumerable<Category> GetUserCategories(string userId)
		=> context.Categories
		//.Where(x => x.UserId == userId)
		.OrderBy(x => x.Name)
		.ToList();

	//public Category GetCategory(int categoryId, string userId)
	//=> _context.Categories.Single(x => x.CategoryId == categoryId && x.UserId == userId);
	public Category GetCategory(int categoryId, string userId)
		=> context.Categories.Single(x => x.CategoryId == categoryId);

	public void AddCategory(Category category)
		=> context.Categories.Add(category);

	public void UpdateCategory(Category category)
	{
		var categoryToUpdate = context.Categories.Single(x => x.CategoryId == category.CategoryId);
		categoryToUpdate.Name = category.Name;
	}

	public void DeleteCategory(int categoryId, string userId)
	{
		//var tasksToChange = _context.Tasks
		//	.Where(x => x.CategoryId == categoryId && x.UserId == userId)
		//	.ToList();

		//var defaultCategoryId = _context.Categories
		//	.Where(x => x.IsDefault == true)
		//	.OrderBy(x => x.CategoryId)
		//	.First()
		//	.CategoryId;

		//foreach (var task in tasksToChange)
		//{
		//	task.CategoryId = defaultCategoryId;
		//}

		//var categoryToDelete = _context.Categories.Single(x => x.CategoryId == categoryId && x.UserId == userId);

		//_context.Categories.Remove(categoryToDelete);
	}
}