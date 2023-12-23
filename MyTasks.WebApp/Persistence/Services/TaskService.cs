using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core.Models.Domains;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Persistence.Services;

public class TaskService
{
	private readonly UnitOfWork _unitOfWork;

	public TaskService(UnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public IEnumerable<Task> GetTasks(string? userId, bool isExecuted = false, int categoryId = 0, string title = null)
	{
		return _unitOfWork.TaskRepository.GetTasks(userId, isExecuted, categoryId, title);
	}

	public IEnumerable<Category> GetCategories()
	{
		return _unitOfWork.TaskRepository.GetCategories();
	}

	public Task GetTask(int taskId, string userId)
	{
		return _unitOfWork.TaskRepository.GetTask(taskId, userId);
	}

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
		// INFO - serwisy - tutaj można umieszczać dodatkową logikę żeby kontrolery pozostały możliwie jak najprostsze
		// dodatkowa logika np. wysłanie email
		// jakieś obliczenia/zmiany w bazie
		_unitOfWork.Complete();
	}
}