using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Repositories;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Persistence.Repositories;

public class TaskRepository(IApplicationDbContext context) : ITaskRepository
{
	public IEnumerable<Task> GetTasks(GetTasksParams getTasksParams)
	{
		var tasks = context.Tasks
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
	{
		var taskToDelete = context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId);
		context.Tasks.Remove(taskToDelete);
	}

	public void FinishTask(int taskId, string userId)
	{
		var taskToFinish = context.Tasks.Single(x => x.TaskId == taskId && x.UserId == userId);
		taskToFinish.IsExecuted = true;
	}
}