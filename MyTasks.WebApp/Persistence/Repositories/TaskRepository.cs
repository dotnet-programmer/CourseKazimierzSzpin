using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Repositories;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Persistence.Repositories;

public class TaskRepository(IApplicationDbContext context) : ITaskRepository
{
	public IEnumerable<Task> GetTasks(GetTasksParams tasksParams)
	{
		var tasks = context.Tasks
			.Include(x => x.Category)
			.Where(x => x.UserId == tasksParams.UserId && x.IsExecuted == tasksParams.FilterTasks.IsExecuted);

		if (tasksParams.FilterTasks.CategoryId != 0)
		{
			tasks = tasks.Where(x => x.CategoryId == tasksParams.FilterTasks.CategoryId);
		}

		if (!string.IsNullOrWhiteSpace(tasksParams.FilterTasks.Title))
		{
			tasks = tasks.Where(x => x.Title.Contains(tasksParams.FilterTasks.Title));
		}

		return tasks.OrderBy(x => x.Term).ToList();
	}

	public Task GetTask(int taskId, string userId)
		=> context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId);

	public void AddTask(Task task)
		=> context.Tasks.Add(task);

	public void UpdateTask(Task task)
	{
		var taskToUpdate = context.Tasks.Single(x => x.TaskId == task.TaskId);
		taskToUpdate.Title = task.Title;
		taskToUpdate.Description = task.Description;
		taskToUpdate.Term = task.Term;
		taskToUpdate.IsExecuted = task.IsExecuted;
		taskToUpdate.CategoryId = task.CategoryId;
	}

	public void DeleteTask(int taskId, string userId)
		=> context.Tasks.Remove(context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId));

	public void FinishTask(int taskId, string userId)
	{
		var taskToFinish = context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId);
		taskToFinish.IsExecuted = true;
	}
}