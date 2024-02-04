using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApp.Core.Services;
using MyTasks.WebApp.Core.ViewModels;
using MyTasks.WebApp.Persistence.Extensions;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Controllers;

[Authorize]
public class TaskController : Controller
{
	private readonly ITaskService _taskService;
	private readonly ICategoryService _categoryService;

	// context jest przekazywany przez Dependency Injection
	public TaskController(ITaskService taskService, ICategoryService categoryService)
	{
		_taskService = taskService;
		_categoryService = categoryService;
	}

	public IActionResult Tasks()
	{
		TasksViewModel vm = new()
		{
			Tasks = _taskService.GetTasks(new() { UserId = User.GetUserId() }),
			Categories = _categoryService.GetCategories(User.GetUserId()),
			FilterTasks = new(),
		};

		return View(vm);
	}

	[HttpPost]
	public IActionResult Tasks(TasksViewModel tasksViewModel)
	{
		var tasks = _taskService.GetTasks(new()
		{
			UserId = User.GetUserId(),
			IsExecuted = tasksViewModel.FilterTasks.IsExecuted,
			CategoryId = tasksViewModel.FilterTasks.CategoryId,
			Title = tasksViewModel.FilterTasks.Title,
		});

		return PartialView("_TasksTable", tasks);
	}

	public IActionResult Task(int taskId = 0)
	{
		var userId = User.GetUserId();

		Task task = taskId == 0 ?
			new Task { TaskId = taskId, UserId = userId, Term = DateTime.Today } :
			_taskService.GetTask(taskId, userId);

		return View(GetTaskViewModel(task));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Task(Task task)
	{
		task.UserId = User.GetUserId();

		if (!ModelState.IsValid)
		{
			TaskViewModel vm = GetTaskViewModel(task);
			return View("Task", vm);
		}

		if (task.TaskId == 0)
		{
			_taskService.AddTask(task);
		}
		else
		{
			_taskService.UpdateTask(task);
		}

		return RedirectToAction("Tasks");
	}

	[HttpPost]
	public IActionResult DeleteTask(int taskId)
	{
		try
		{
			var userId = User.GetUserId();
			_taskService.DeleteTask(taskId, userId);
		}
		catch (Exception ex)
		{
			// logowanie do pliku
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}

	[HttpPost]
	public IActionResult FinishTask(int taskId)
	{
		try
		{
			var userId = User.GetUserId();
			_taskService.FinishTask(taskId, userId);
		}
		catch (Exception ex)
		{
			// logowanie do pliku
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}

	private TaskViewModel GetTaskViewModel(Task task) => new()
	{
		Task = task,
		Heading = task.TaskId == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
		Categories = _categoryService.GetCategories(User.GetUserId())
	};
}