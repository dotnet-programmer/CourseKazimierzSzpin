using MyTasks.WebApp.Persistence.Repositories;

namespace MyTasks.WebApp.Persistence;

public class UnitOfWork
{
	private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
		_context = context;
    }

    public TaskRepository TaskRepository { get; set; }

	public void Complete()
	{
		_context.SaveChanges();
	}
}