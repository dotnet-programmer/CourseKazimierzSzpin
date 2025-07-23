using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Repositories;

namespace MyTasks.WebApp.Persistence.Repositories;

public class CategoryRepository(IApplicationDbContext context) : ICategoryRepository
{
	public IEnumerable<Category> GetCategories(string userId)
		=> context.Categories
			.Where(x => x.UserId == userId)
			.OrderBy(x => x.Name)
			.ToList();

	public Category GetCategory(int categoryId, string userId)
		=> context.Categories.Single(x => x.CategoryId == categoryId && x.UserId == userId);

	public void AddCategory(Category category)
		=> context.Categories.Add(category);

	public void UpdateCategory(Category category)
	{
		var categoryToUpdate = context.Categories.Single(x => x.CategoryId == category.CategoryId);
		categoryToUpdate.Name = category.Name;
	}

	public void DeleteCategory(int categoryId, string userId)
		=> context.Categories.Remove(context.Categories.Single(x => x.CategoryId == categoryId && x.UserId == userId));
}