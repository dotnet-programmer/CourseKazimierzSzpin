using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Services;
using MyTasks.WebApp.Core.ViewModels;
using MyTasks.WebApp.Persistence.Extensions;

namespace MyTasks.WebApp.Controllers;

[Authorize]
public class CategoryController : Controller
{
	private readonly ICategoryService _categoryService;

	public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

	public IActionResult Categories() => View(_categoryService.GetUserCategories(User.GetUserId()));

	public IActionResult Category(int categoryId = 0)
	{
		var userId = User.GetUserId();

		Category category = categoryId == 0 ?
			new Category { CategoryId = categoryId, UserId = userId } :
			_categoryService.GetCategory(categoryId, userId);

		return View(GetCategoryViewModel(category));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Category(Category category)
	{
		category.UserId = User.GetUserId();

		// INFO - walidacja ModelState - usunięcie pola z elementów sprawdzanych przez ModelState.IsValid
		ModelState.Remove("Category.UserId");

		if (!ModelState.IsValid)
		{
			CategoryViewModel vm = GetCategoryViewModel(category);
			return View("Category", vm);
		}

		if (category.CategoryId == 0)
		{
			_categoryService.AddCategory(category);
		}
		else
		{
			_categoryService.UpdateCategory(category);
		}

		return RedirectToAction("Categories");
	}

	[HttpPost]
	public IActionResult DeleteCategory(int categoryId)
	{
		try
		{
			var userId = User.GetUserId();
			_categoryService.DeleteCategory(categoryId, userId);
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}

	private CategoryViewModel GetCategoryViewModel(Category category) => new()
	{
		Category = category,
		Heading = category.CategoryId == 0 ? "Dodawanie nowej kategorii" : "Edytowanie kategorii",
	};
}