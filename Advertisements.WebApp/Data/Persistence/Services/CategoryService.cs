using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Services;

namespace Advertisements.WebApp.Data.Persistence.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
	public IEnumerable<Category> GetCategories()
		=> unitOfWork.CategoryRepository.GetCategories();

	public IEnumerable<Category> GetUserCategories(string userId)
		=> unitOfWork.CategoryRepository.GetUserCategories(userId);

	public Category GetCategory(int categoryId, string userId)
		=> unitOfWork.CategoryRepository.GetCategory(categoryId, userId);

	public void AddCategory(Category category)
	{
		unitOfWork.CategoryRepository.AddCategory(category);
		unitOfWork.Complete();
	}

	public void UpdateCategory(Category category)
	{
		unitOfWork.CategoryRepository.UpdateCategory(category);
		unitOfWork.Complete();
	}

	public void DeleteCategory(int categoryId, string userId)
	{
		unitOfWork.CategoryRepository.DeleteCategory(categoryId, userId);
		unitOfWork.Complete();
	}
}