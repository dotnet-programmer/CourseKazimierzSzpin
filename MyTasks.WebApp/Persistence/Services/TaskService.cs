using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Services;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Persistence.Services;

// INFO - serwisy - tutaj można umieszczać dodatkową logikę żeby kontrolery pozostały możliwie jak najprostsze
// dodatkowa logika np. wysłanie email
// jakieś obliczenia/zmiany w bazie
public class TaskService : ITaskService
{
	private readonly IUnitOfWork _unitOfWork;

	public TaskService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

	// INFO - sygnatury metod skopiowane z TaskRepository
	public IEnumerable<Task> GetTasks(GetTasksParams getTasksParams) => _unitOfWork.TaskRepository.GetTasks(getTasksParams);

	public IEnumerable<Category> GetCategories() => _unitOfWork.TaskRepository.GetCategories();

	public Task GetTask(int taskId, string userId) => _unitOfWork.TaskRepository.GetTask(taskId, userId);

	public void AddTask(Task task)
	{
		_unitOfWork.TaskRepository.AddTask(task);
		_unitOfWork.Complete();
	}

	public void UpdateTask(Task task)
	{
		_unitOfWork.TaskRepository.UpdateTask(task);
		_unitOfWork.Complete();
	}

	public void DeleteTask(int taskId, string userId)
	{
		_unitOfWork.TaskRepository.DeleteTask(taskId, userId);
		_unitOfWork.Complete();
	}

	public void FinishTask(int taskId, string userId)
	{
		_unitOfWork.TaskRepository.FinishTask(taskId, userId);
		// wyślij email
		// inna logika
		// inne zmiany w bazie danych
		_unitOfWork.Complete();
	}
}