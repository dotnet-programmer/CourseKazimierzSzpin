using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Services;
using MyTasks.WebApp.Core.ViewModels;
using MyTasks.WebApp.Persistence.Extensions;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Controllers;

[Authorize]
public class TaskController : Controller
{
	private readonly ITaskService _taskService;

	// context jest przekazywany przez Dependency Injection
	public TaskController(ITaskService taskService) => _taskService = taskService;

	public IActionResult Tasks()
	{
		TasksViewModel vm = new()
		{
			Tasks = _taskService.GetTasks(new() { UserId = User.GetUserId() }),
			Categories = _taskService.GetCategories(),
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

	public IActionResult Categories()
	{
		return View(_taskService.GetCategories(User.GetUserId()));
	}

	public IActionResult Category(int categoryId = 0)
	{
		var userId = User.GetUserId();

		Category category = categoryId == 0 ?
			new Category { CategoryId = categoryId, UserId = userId } :
			_taskService.GetCategory(categoryId, userId);

		return View(GetCategoryViewModel(category));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Category(Category category)
	{
		category.UserId = User.GetUserId();

		if (!ModelState.IsValid)
		{
			CategoryViewModel vm = GetCategoryViewModel(category);
			return View("Category", vm);
		}

		if (category.CategoryId == 0)
		{
			_taskService.AddCategory(category);
		}
		else
		{
			_taskService.UpdateCategory(category);
		}

		return RedirectToAction("Categories");
	}

	// TODO: po usunięciu kategorii zadanie powinno zostać na liście tylko bez żadnej kategorii
	// teraz robi się kaskadowe usuwanie, po usunięciu kategorii usuwane są wpisy powiązane z tą kategorią
	// czyli zrobić tak jak w zadaniu żeby była kategoria "ogólna":
	//	albo która jest przypisywana do każdego nowego usera (wtedy mając 1000 użytkowników będzie 1000 takich samych wpisów)
	//	albo tylko 1 wpis w bazie, bez id usera, nieedytowalna i nieusuwalna
	[HttpPost]
	public IActionResult DeleteCategory(int categoryId)
	{
		try
		{
			var userId = User.GetUserId();
			_taskService.DeleteCategory(categoryId, userId);
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
		Categories = _taskService.GetCategories()
	};

	private CategoryViewModel GetCategoryViewModel(Category category) => new()
	{
		Category = category,
		Heading = category.CategoryId == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
	};
}