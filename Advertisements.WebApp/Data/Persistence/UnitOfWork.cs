using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Repositories;
using Advertisements.WebApp.Data.Persistence.Repositories;

namespace Advertisements.WebApp.Data.Persistence;

public class UnitOfWork : IUnitOfWork
{
	private readonly IApplicationDbContext _context;

	public UnitOfWork(IApplicationDbContext context)
	{
		_context = context;
		AdvertisementRepository = new AdvertisementRepository(context);
		CategoryRepository = new CategoryRepository(context);
	}

	public IAdvertisementRepository AdvertisementRepository { get; }
	public ICategoryRepository CategoryRepository { get; }

	public void Complete() => _context.SaveChanges();
}