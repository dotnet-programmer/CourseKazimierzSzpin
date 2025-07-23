using MyTasks.WebApp.Core.Models.Domains;

namespace MyTasks.WebApp.Core.Repositories;

public interface ICategoryRepository
{
	IEnumerable<Category> GetCategories(string userId);
	Category GetCategory(int categoryId, string userId);
	void AddCategory(Category category);
	void UpdateCategory(Category category);
	void DeleteCategory(int categoryId, string userId);
}