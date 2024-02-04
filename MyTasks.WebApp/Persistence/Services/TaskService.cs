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

	public IEnumerable<Category> GetCategories() => _unitOfWork.TaskRepository.GetCategories();

	public IEnumerable<Category> GetCategories(string userId) => _unitOfWork.TaskRepository.GetCategories(userId);

	public IEnumerable<Category> GetUserCategories(string userId) => _unitOfWork.TaskRepository.GetUserCategories(userId);

	public Category GetCategory(int categoryId, string userId) => _unitOfWork.TaskRepository.GetCategory(categoryId, userId);

	public void AddCategory(Category category)
	{
		_unitOfWork.TaskRepository.AddCategory(category);
		_unitOfWork.Complete();
	}

	public void UpdateCategory(Category category)
	{
		_unitOfWork.TaskRepository.UpdateCategory(category);
		_unitOfWork.Complete();
	}

	public void DeleteCategory(int categoryId, string userId)
	{
		_unitOfWork.TaskRepository.DeleteCategory(categoryId, userId);
		_unitOfWork.Complete();
	}
}