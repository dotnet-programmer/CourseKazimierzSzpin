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
		=> View(GetTasksViewModel());

	// akcja wyświetlająca filtrowane zadania, czyli wynik wyszukiwania 
	// PartialView bo zwracany widok częściowy, czyli sam HTML
	[HttpPost]
	public IActionResult Tasks(TasksViewModel tasksViewModel)
		=> PartialView("_TasksTable", GetFilteredTasks(tasksViewModel));

	public IActionResult Task(int taskId = 0)
		=> View(GetTaskViewModel(taskId));

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Task(Task task)
	{
		task.UserId = User.GetUserId();

		if (!ModelState.IsValid)
		{
			return View("Task", GetTaskViewModel(task));
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
	public IActionResult DeleteTask(int id)
	{
		try
		{
			taskService.DeleteTask(id, User.GetUserId());
		}
		catch (Exception ex)
		{
			// logowanie do pliku
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}

	[HttpPost]
	public IActionResult FinishTask(int id)
	{
		try
		{
			taskService.FinishTask(id, User.GetUserId());
		}
		catch (Exception ex)
		{
			// logowanie do pliku
			return Json(new { success = false, message = ex.Message });
		}

		return Json(new { success = true });
	}

	private TasksViewModel GetTasksViewModel()
		=> new()
		{
			Tasks = taskService.GetTasks(new GetTasksParams() { UserId = User.GetUserId() }),
			Categories = categoryService.GetCategories(User.GetUserId()),
			FilterTasks = new(),
		};

	private IEnumerable<Task> GetFilteredTasks(TasksViewModel tasksViewModel)
		=> taskService.GetTasks(new GetTasksParams()
		{
			UserId = User.GetUserId(),
			FilterTasks = tasksViewModel.FilterTasks
		});

	private TaskViewModel GetTaskViewModel(int taskId)
	{
		var userId = User.GetUserId();

		Task task = taskId == 0 ?
			new Task { TaskId = taskId, UserId = userId, Term = DateTime.Today } :
			taskService.GetTask(taskId, userId);

		return GetTaskViewModel(task);
	}

	private TaskViewModel GetTaskViewModel(Task task)
		=> new()
		{
			Task = task,
			Heading = task.TaskId == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
			Categories = categoryService.GetCategories(User.GetUserId())
		};
}