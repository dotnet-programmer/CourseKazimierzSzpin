using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Services;

namespace MyTasks.WebApp.Persistence.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
	public IEnumerable<Category> GetCategories(string userId)
		=> unitOfWork.CategoryRepository.GetCategories(userId);

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