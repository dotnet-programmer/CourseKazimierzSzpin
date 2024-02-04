using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Repositories;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
	private readonly IApplicationDbContext _context;

	public TaskRepository(IApplicationDbContext context) => _context = context;

	public IEnumerable<Task> GetTasks(GetTasksParams getTasksParams)
	{
		var tasks = _context.Tasks
			.Include(x => x.Category)
			.Where(x => x.UserId == getTasksParams.UserId && x.IsExecuted == getTasksParams.IsExecuted);

		if (getTasksParams.CategoryId != 0)
		{
			tasks = tasks.Where(x => x.CategoryId == getTasksParams.CategoryId);
		}

		if (!string.IsNullOrWhiteSpace(getTasksParams.Title))
		{
			tasks = tasks.Where(x => x.Title.Contains(getTasksParams.Title));
		}

		return tasks.OrderBy(x => x.Term).ToList();
	}

	public Task GetTask(int taskId, string userId) => _context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId);

	public void AddTask(Task task) => _context.Tasks.Add(task);

	public void UpdateTask(Task task)
	{
		var taskToUpdate = _context.Tasks.Single(x => x.TaskId == task.TaskId);
		taskToUpdate.Title = task.Title;
		taskToUpdate.Description = task.Description;
		taskToUpdate.Term = task.Term;
		taskToUpdate.IsExecuted = task.IsExecuted;
		taskToUpdate.CategoryId = task.CategoryId;
	}

	public void DeleteTask(int taskId, string userId)
	{
		var taskToDelete = _context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId);
		_context.Tasks.Remove(taskToDelete);
	}

	public void FinishTask(int taskId, string userId)
	{
		var taskToFinish = _context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId);
		taskToFinish.IsExecuted = true;
	}

	public IEnumerable<Category> GetCategories() =>
		_context.Categories
		.OrderBy(x => x.Name)
		.ToList();

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

	public Category GetCategory(int categoryId, string userId) => _context.Categories.Single(x=> x.CategoryId == categoryId && x.UserId == userId);

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