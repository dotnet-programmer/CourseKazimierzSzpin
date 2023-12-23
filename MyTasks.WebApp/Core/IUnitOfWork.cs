using MyTasks.WebApp.Core.Repositories;

namespace MyTasks.WebApp.Core;

public interface IUnitOfWork
{
	ITaskRepository TaskRepository { get; }

	void Complete();
}