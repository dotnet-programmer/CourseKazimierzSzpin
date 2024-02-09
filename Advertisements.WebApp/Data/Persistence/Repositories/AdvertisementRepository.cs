using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Models;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Advertisements.WebApp.Data.Persistence.Repositories;

public class AdvertisementRepository : IAdvertisementRepository
{
	private readonly IApplicationDbContext _context;

	public AdvertisementRepository(IApplicationDbContext context) => _context = context;

	public IEnumerable<Advertisement> GetAdvertisements(GetAdvertisementsParams getAdvertisementsParams)
	{
		var advertisements = _context.Advertisements.Where(x => x.IsArchival == getAdvertisementsParams.IsArchival && x.Price >= getAdvertisementsParams.MinPrice);

		if (getAdvertisementsParams.MaxPrice != 0)
		{
			advertisements = advertisements.Where(x => x.Price <= getAdvertisementsParams.MaxPrice);
		}

		if (getAdvertisementsParams.CategoryId != 0)
		{
			advertisements = advertisements.Where(x => x.CategoryId == getAdvertisementsParams.CategoryId);
		}

		//if (getAdvertisementsParams.SubcategoryId != 0)
		//{
		//	advertisements = advertisements.Where(x => x.SubcategoryId == getAdvertisementsParams.SubcategoryId);
		//}

		if (!string.IsNullOrWhiteSpace(getAdvertisementsParams.Title))
		{
			advertisements = advertisements.Where(x => x.Title.Contains(getAdvertisementsParams.Title));
		}

		return advertisements.OrderByDescending(x => x.Added).ToList();
	}

	public Advertisement GetAdvertisement(int advertisementId, string userId) => _context.Advertisements.Single(x=>x.AdvertisementId == advertisementId && x.UserId == userId);

	public void AddAdvertisement(Advertisement advertisement) => _context.Advertisements.Add(advertisement);

	public void UpdateAdvertisement(Advertisement advertisement)
	{
		var advertisementToUpdate = _context.Advertisements.Single(x => x.AdvertisementId == advertisement.AdvertisementId);
		advertisementToUpdate.Title = advertisement.Title;
		advertisementToUpdate.Description = advertisement.Description;
		advertisementToUpdate.Price = advertisement.Price;
		advertisementToUpdate.City = advertisement.City;
		advertisementToUpdate.Picture = advertisement.Picture;
		advertisementToUpdate.PictureFormat = advertisement.PictureFormat;
		//advertisementToUpdate.SubcategoryId = advertisement.SubcategoryId;
		advertisementToUpdate.CategoryId = advertisement.CategoryId;
	}

	public void FinishAdvertisement(int advertisementId, string userId) => throw new NotImplementedException();

	public void DeleteAdvertisement(int advertisementId, string userId) => throw new NotImplementedException();
}