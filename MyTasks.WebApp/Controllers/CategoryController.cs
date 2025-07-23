using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Services;
using MyTasks.WebApp.Core.ViewModels;
using MyTasks.WebApp.Persistence.Extensions;

namespace MyTasks.WebApp.Controllers;

[Authorize]
public class CategoryController(ICategoryService categoryService) : Controller
{
	public IActionResult Categories()
		=> View(categoryService.GetCategories(User.GetUserId()));

	public IActionResult Category(int categoryId = 0)
		=> View(GetCategoryViewModel(categoryId));

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Category(Category category)
	{
		category.UserId = User.GetUserId();

		// INFO - walidacja ModelState - usunięcie pola z elementów sprawdzanych przez ModelState.IsValid
		// dodanie wartości tutaj w kodzie nie powoduje zmiany stanu, jeśli z widoku przyszła wartość null
		ModelState.Remove("category.UserId");

		if (!ModelState.IsValid)
		{
			return View("Category", GetCategoryViewModel(category));
		}

		if (category.CategoryId == 0)
		{
			categoryService.AddCategory(category);
		}
		else
		{
			categoryService.UpdateCategory(category);
		}

		return RedirectToAction("Categories");
	}

	[HttpPost]
	public IActionResult DeleteCategory(int id)
	{
		try
		{
			categoryService.DeleteCategory(id, User.GetUserId());
		}
		catch (Exception ex)
		{
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}


	private CategoryViewModel GetCategoryViewModel(int categoryId)
	{
		var userId = User.GetUserId();

		Category category = categoryId == 0 ?
			new Category { UserId = userId } :
			categoryService.GetCategory(categoryId, userId);

		return GetCategoryViewModel(category);
	}

	private CategoryViewModel GetCategoryViewModel(Category category)
		=> new()
		{
			Category = category,
			Heading = category.CategoryId == 0 ? "Dodawanie nowej kategorii" : "Edytowanie kategorii",
		};
}