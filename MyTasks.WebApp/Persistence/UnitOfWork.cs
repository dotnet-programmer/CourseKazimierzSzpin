using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Repositories;
using MyTasks.WebApp.Persistence.Repositories;

namespace MyTasks.WebApp.Persistence;

public class UnitOfWork(IApplicationDbContext context) : IUnitOfWork
{
	// INFO - obiekty repozytoryjne, jeżeli więcej repozytoriów, to więcej takich właściwości
	public ITaskRepository TaskRepository { get; } = new TaskRepository(context);
	public ICategoryRepository CategoryRepository { get; } = new CategoryRepository(context);

	public void Complete() 
		=> context.SaveChanges();
}