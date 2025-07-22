using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Services;
using MyTasks.WebApp.Core.ViewModels;
using MyTasks.WebApp.Persistence.Extensions;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Controllers;

[Authorize]
public class TaskController(ITaskService taskService, ICategoryService categoryService) : Controller
{
	// akcja wyświetlana jako strona główna ze wszystkimi zadaniami
	public IActionResult Tasks()
		=> View(new TasksViewModel()
		{
			Tasks = taskService.GetTasks(new GetTasksParams() { UserId = User.GetUserId() }),
			Categories = categoryService.GetCategories(User.GetUserId()),
			FilterTasks = new(),
		});

	[HttpPost]
	public IActionResult Tasks(TasksViewModel tasksViewModel)
	{
		var tasks = taskService.GetTasks(new GetTasksParams()
		{
			UserId = User.GetUserId(),
			IsExecuted = tasksViewModel.FilterTasks.IsExecuted,
			CategoryId = tasksViewModel.FilterTasks.CategoryId,
			Title = tasksViewModel.FilterTasks.Title,
		});

		// PartialView bo zwracany widok częściowy, czyli sam HTML
		return PartialView("_TasksTable", tasks);
	}

	public IActionResult Task(int taskId = 0)
	{
		// TODO: przenieść całość do GetTaskViewModel(taskId)
		var userId = User.GetUserId();

		Task task = taskId == 0 ?
			new Task { TaskId = taskId, UserId = userId, Term = DateTime.Today } :
			taskService.GetTask(taskId, userId);

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
			taskService.AddTask(task);
		}
		else
		{
			taskService.UpdateTask(task);
		}

		return RedirectToAction("Tasks");
	}

	[HttpPost]
	public IActionResult DeleteTask(int taskId)
	{
		try
		{
			taskService.DeleteTask(taskId, User.GetUserId());
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
			taskService.FinishTask(taskId, User.GetUserId());
		}
		catch (Exception ex)
		{
			// logowanie do pliku
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}

	private TaskViewModel GetTaskViewModel(Task task)
		=> new()
		{
			Task = task,
			Heading = task.TaskId == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
			Categories = categoryService.GetCategories(User.GetUserId())
		};
}