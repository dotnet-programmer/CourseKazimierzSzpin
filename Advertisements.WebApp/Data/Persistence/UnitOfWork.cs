using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Repositories;
using Advertisements.WebApp.Data.Persistence.Repositories;

namespace Advertisements.WebApp.Data.Persistence;

public class UnitOfWork(IApplicationDbContext context) : IUnitOfWork
{
	public IAdvertisementRepository AdvertisementRepository { get; } = new AdvertisementRepository(context);
	public ICategoryRepository CategoryRepository { get; } = new CategoryRepository(context);

	public void Complete()
		=> context.SaveChanges();
}