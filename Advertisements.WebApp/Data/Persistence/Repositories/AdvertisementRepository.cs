using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Models;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Repositories;

namespace Advertisements.WebApp.Data.Persistence.Repositories;

public class AdvertisementRepository : IAdvertisementRepository
{
	private readonly IApplicationDbContext _context;

	public AdvertisementRepository(IApplicationDbContext context) => _context = context;

	public IEnumerable<Advertisement> GetAdvertisements(GetAdvertisementsParams getAdvertisementsParams) => throw new NotImplementedException();

	public Advertisement GetAdvertisement(int advertisementId, string userId) => _context.Advertisements.Single(x=>x.AdvertisementId == advertisementId && x.UserId == userId);

	public void AddAdvertisement(Advertisement advertisement) => _context.Advertisements.Add(advertisement);

	public void UpdateAdvertisement(Advertisement advertisement)
	{
		var advertisementToUpdate = _context.Advertisements.Single(x => x.AdvertisementId == advertisement.AdvertisementId);
		advertisementToUpdate.City = advertisement.City;
		advertisementToUpdate.Description = advertisement.Description;
		advertisementToUpdate.Picture = advertisement.Picture;
		advertisementToUpdate.PictureFormat = advertisement.PictureFormat;
		advertisementToUpdate.SubcategoryId = advertisement.SubcategoryId;
		advertisementToUpdate.Title = advertisement.Title;
	}

	public void FinishAdvertisement(int advertisementId, string userId) => throw new NotImplementedException();

	public void DeleteAdvertisement(int advertisementId, string userId) => throw new NotImplementedException();
}