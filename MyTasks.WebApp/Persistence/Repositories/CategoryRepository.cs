using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Repositories;

namespace MyTasks.WebApp.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
	private readonly IApplicationDbContext _context;

	public CategoryRepository(IApplicationDbContext context) => _context = context;

	public IEnumerable<Category> GetCategories(string userId) =>
		_context.Categories
		.Where(x => x.IsDefault == true || x.UserId == userId)
		.OrderBy(x => x.Name)
		.ToList();

	public IEnumerable<Category> GetUserCategories(string userId) =>
		_context.Categories
		.Where(x => x.UserId == userId)
		.OrderBy(x => x.Name)
		.ToList();

	public Category GetCategory(int categoryId, string userId) => _context.Categories.Single(x => x.CategoryId == categoryId && x.UserId == userId);

	public void AddCategory(Category category) => _context.Categories.Add(category);

	public void UpdateCategory(Category category)
	{
		var categoryToUpdate = _context.Categories.Single(x => x.CategoryId == category.CategoryId);
		categoryToUpdate.Name = category.Name;
	}

	public void DeleteCategory(int categoryId, string userId)
	{
		var tasksToChange = _context.Tasks
			.Where(x => x.CategoryId == categoryId && x.UserId == userId)
			.ToList();

		var defaultCategoryId = _context.Categories
			.Where(x => x.IsDefault == true)
			.OrderBy(x => x.CategoryId)
			.First()
			.CategoryId;

		foreach (var task in tasksToChange)
		{
			task.CategoryId = defaultCategoryId;
		}

		var categoryToDelete = _context.Categories.Single(x => x.CategoryId == categoryId && x.UserId == userId);

		_context.Categories.Remove(categoryToDelete);
	}
}