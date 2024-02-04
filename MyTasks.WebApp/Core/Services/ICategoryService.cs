﻿using MyTasks.WebApp.Core.Models.Domains;

namespace MyTasks.WebApp.Core.Services;

public interface ICategoryService
{
	IEnumerable<Category> GetCategories(string userId);

	IEnumerable<Category> GetUserCategories(string userId);

	Category GetCategory(int categoryId, string userId);

	void AddCategory(Category category);

	void UpdateCategory(Category category);

	void DeleteCategory(int categoryId, string userId);
}