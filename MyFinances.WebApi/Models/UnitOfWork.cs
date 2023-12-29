using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.Repositories;

namespace MyFinances.WebApi.Models;

public class UnitOfWork
{
	private readonly MyFinancesContext _context;

	public UnitOfWork(MyFinancesContext context)
	{
		_context = context;
		OperationRepository = new(context);
	}

    public OperationRepository OperationRepository { get; }

	public void Complete()
	{
		_context.SaveChanges();
	}
}