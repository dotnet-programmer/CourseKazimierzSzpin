
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core.Models.Domains;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Persistence.Repositories;

public class TaskRepository
{
	private ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
		_context = context;
    }

    public IEnumerable<Task> GetTasks(string? userId, bool isExecuted = false, int categoryId = 0, string title = null)
	{
		var tasks = _context.Tasks
			.Include(x => x.Category)
			.Where(x => x.UserId == userId && x.IsExecuted == isExecuted);

		if (categoryId != 0)
		{
			tasks = tasks.Where(x => x.CategoryId == categoryId);
		}

		if (!string.IsNullOrWhiteSpace(title))
		{
			tasks = tasks.Where(x => x.Title.Contains(title));
		}

		return tasks.OrderBy(x => x.Term).ToList();
	}

	public IEnumerable<Category> GetCategories() => 
		_context.Categories
		.OrderBy(x => x.Name)
		.ToList();

	public Task GetTask(int taskId, string userId) => _context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId);

	public void AddTask(Task task)
	{
		_context.Tasks.Add(task);
	}

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
}
