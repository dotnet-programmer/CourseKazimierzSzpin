using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Services;

namespace MyTasks.WebApp.Persistence.Services;

public class CategoryService : ICategoryService
{
	private readonly IUnitOfWork _unitOfWork;

	public CategoryService(IUnitOfWork unitOfWork)
		=> _unitOfWork = unitOfWork;

	public IEnumerable<Category> GetCategories(string userId)
		=> _unitOfWork.CategoryRepository.GetCategories(userId);

	public IEnumerable<Category> GetUserCategories(string userId)
		=> _unitOfWork.CategoryRepository.GetUserCategories(userId);

	public Category GetCategory(int categoryId, string userId)
		=> _unitOfWork.CategoryRepository.GetCategory(categoryId, userId);

	public void AddCategory(Category category)
	{
		_unitOfWork.CategoryRepository.AddCategory(category);
		_unitOfWork.Complete();
	}

	public void UpdateCategory(Category category)
	{
		_unitOfWork.CategoryRepository.UpdateCategory(category);
		_unitOfWork.Complete();
	}

	public void DeleteCategory(int categoryId, string userId)
	{
		_unitOfWork.CategoryRepository.DeleteCategory(categoryId, userId);
		_unitOfWork.Complete();
	}
}