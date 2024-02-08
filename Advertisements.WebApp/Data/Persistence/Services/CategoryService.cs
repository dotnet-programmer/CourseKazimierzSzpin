using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Services;

namespace Advertisements.WebApp.Data.Persistence.Services;

public class CategoryService : ICategoryService
{
	private readonly IUnitOfWork _unitOfWork;

	public CategoryService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

	public IEnumerable<Category> GetCategories() => _unitOfWork.CategoryRepository.GetCategories();

	public IEnumerable<Category> GetUserCategories(string userId) => _unitOfWork.CategoryRepository.GetUserCategories(userId);

	public Category GetCategory(int categoryId, string userId) => _unitOfWork.CategoryRepository.GetCategory(categoryId, userId);

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