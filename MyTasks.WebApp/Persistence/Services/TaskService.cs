using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Services;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Persistence.Services;

// INFO - serwisy - tutaj można umieszczać dodatkową logikę żeby kontrolery pozostały możliwie jak najprostsze
// dodatkowa logika np. wysłanie email
// jakieś obliczenia/zmiany w bazie
public class TaskService(IUnitOfWork unitOfWork) : ITaskService
{
	public IEnumerable<Task> GetTasks(GetTasksParams getTasksParams)
		=> unitOfWork.TaskRepository.GetTasks(getTasksParams);

	public Task GetTask(int taskId, string userId)
		=> unitOfWork.TaskRepository.GetTask(taskId, userId);

	public void AddTask(Task task)
	{
		unitOfWork.TaskRepository.AddTask(task);
		unitOfWork.Complete();
	}

	public void UpdateTask(Task task)
	{
		unitOfWork.TaskRepository.UpdateTask(task);
		unitOfWork.Complete();
	}

	public void DeleteTask(int taskId, string userId)
	{
		unitOfWork.TaskRepository.DeleteTask(taskId, userId);
		unitOfWork.Complete();
	}

	public void FinishTask(int taskId, string userId)
	{
		unitOfWork.TaskRepository.FinishTask(taskId, userId);
		// wyślij email
		// inna logika
		// inne zmiany w bazie danych
		unitOfWork.Complete();
	}
}