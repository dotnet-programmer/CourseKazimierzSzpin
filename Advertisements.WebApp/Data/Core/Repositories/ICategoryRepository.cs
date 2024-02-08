using Advertisements.WebApp.Data.Core.Models.Domains;

namespace Advertisements.WebApp.Data.Core.Repositories;

public interface ICategoryRepository
{
	IEnumerable<Category> GetCategories();

	IEnumerable<Category> GetUserCategories(string userId);

	Category GetCategory(int categoryId, string userId);

	void AddCategory(Category category);

	void UpdateCategory(Category category);

	void DeleteCategory(int categoryId, string userId);
}