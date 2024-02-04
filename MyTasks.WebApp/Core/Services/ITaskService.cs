using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Models.Domains;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Core.Services;

public interface ITaskService
{
	IEnumerable<Task> GetTasks(GetTasksParams getTasksParams);

	Task GetTask(int taskId, string userId);

	void AddTask(Task task);

	void UpdateTask(Task task);

	void DeleteTask(int taskId, string userId);

	void FinishTask(int taskId, string userId);

	IEnumerable<Category> GetCategories();

	IEnumerable<Category> GetCategories(string userId);

	IEnumerable<Category> GetUserCategories(string userId);

	Category GetCategory(int categoryId, string userId);

	void AddCategory(Category category);

	void UpdateCategory(Category category);

	void DeleteCategory(int categoryId, string userId);
}