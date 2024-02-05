using Advertisements.WebApp.Data.Core;

namespace Advertisements.WebApp.Data.Persistence;

public class UnitOfWork : IUnitOfWork
{
	private readonly IApplicationDbContext _context;

	public UnitOfWork(IApplicationDbContext context)
	{
		_context = context;
	}
}
