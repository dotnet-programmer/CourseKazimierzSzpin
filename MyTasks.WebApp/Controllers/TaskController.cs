using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.ViewModels;
using MyTasks.WebApp.Persistence;
using MyTasks.WebApp.Persistence.Extensions;
using MyTasks.WebApp.Persistence.Repositories;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Controllers;

[Authorize]
public class TaskController : Controller
{
	private readonly TaskRepository _taskRepository;

    public TaskController(ApplicationDbContext context)
    {
		_taskRepository = new(context);
	}

    public IActionResult Tasks()
	{
		TasksViewModel vm = new()
		{
			Tasks = _taskRepository.GetTasks(User.GetUserId()),
			Categories = _taskRepository.GetCategories(),
			FilterTasks = new(),
		};
		return View(vm);
	}

	[HttpPost]
	public IActionResult Tasks(TasksViewModel tasksViewModel)
	{
		var tasks = _taskRepository.GetTasks(
			User.GetUserId(),
			tasksViewModel.FilterTasks.IsExecuted,
			tasksViewModel.FilterTasks.CategoryId,
			tasksViewModel.FilterTasks.Title);
		return PartialView("_TasksTable", tasks);
	}

	public IActionResult Task(int taskId = 0)
	{
		var userId = User.GetUserId();
		Task task = taskId == 0 ? 
			new Task { TaskId = taskId, UserId = userId, Term = DateTime.Today } : 
			_taskRepository.GetTask(taskId, userId);
		TaskViewModel vm = new()
		{
			Task = task,
			Heading = taskId == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
			Categories = _taskRepository.GetCategories()
		};
		return View(vm);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Task(Task task)
	{
		task.UserId = User.GetUserId();
		if (!ModelState.IsValid)
		{
			TaskViewModel vm = new()
			{
				Task = task,
				Heading = task.TaskId == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
				Categories = _taskRepository.GetCategories()
			};
			return View("Task", vm);
		}
		if (task.TaskId == 0)
		{
			_taskRepository.AddTask(task);
		}
		else
		{
			_taskRepository.UpdateTask(task);
		}
		return RedirectToAction("Tasks");
	}

	[HttpPost]
	public IActionResult DeleteTask(int taskId)
	{
		try
		{
			var userId = User.GetUserId();
			_taskRepository.DeleteTask(taskId, userId);
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
			_taskRepository.FinishTask(taskId, userId);
		}
		catch (Exception ex)
		{
			// logowanie do pliku
			return Json(new { success = false, message = ex.Message });
		}
		return Json(new { success = true });
	}
}
