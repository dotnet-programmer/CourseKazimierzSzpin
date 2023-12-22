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
	private UnitOfWork _unitOfWork;

	// context jest przekazywany przez Dependency Injection
	public TaskController(ApplicationDbContext context)
    {
		_unitOfWork = new(context);
	}

    public IActionResult Tasks()
	{
		TasksViewModel vm = new()
		{
			Tasks = _unitOfWork.TaskRepository.GetTasks(User.GetUserId()),
			Categories = _unitOfWork.TaskRepository.GetCategories(),
			FilterTasks = new(),
		};
		return View(vm);
	}

	[HttpPost]
	public IActionResult Tasks(TasksViewModel tasksViewModel)
	{
		var tasks = _unitOfWork.TaskRepository.GetTasks(
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
			_unitOfWork.TaskRepository.GetTask(taskId, userId);
		TaskViewModel vm = new()
		{
			Task = task,
			Heading = taskId == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
			Categories = _unitOfWork.TaskRepository.GetCategories()
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
				Categories = _unitOfWork.TaskRepository.GetCategories()
			};
			return View("Task", vm);
		}
		if (task.TaskId == 0)
		{
			_unitOfWork.TaskRepository.AddTask(task);
		}
		else
		{
			_unitOfWork.TaskRepository.UpdateTask(task);
		}
		_unitOfWork.Complete();
		return RedirectToAction("Tasks");
	}

	[HttpPost]
	public IActionResult DeleteTask(int taskId)
	{
		try
		{
			var userId = User.GetUserId();
			_unitOfWork.TaskRepository.DeleteTask(taskId, userId);
			_unitOfWork.Complete();
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
			_unitOfWork.TaskRepository.FinishTask(taskId, userId);
			_unitOfWork.Complete();
		}
		catch (Exception ex)
		{
			// logowanie do pliku
			return Json(new { success = false, message = ex.Message });
		}
		return Json(new { success = true });
	}
}
