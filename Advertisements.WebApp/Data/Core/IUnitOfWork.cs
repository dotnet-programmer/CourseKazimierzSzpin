using Advertisements.WebApp.Data.Core.Repositories;

namespace Advertisements.WebApp.Data.Core;

public interface IUnitOfWork
{
	IAdvertisementRepository AdvertisementRepository { get; }
	ICategoryRepository CategoryRepository { get; }
	void Complete();
}