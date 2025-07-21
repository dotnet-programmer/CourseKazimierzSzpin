using Advertisements.WebApp.Data.Core.Models;
using Advertisements.WebApp.Data.Core.Models.Domains;

namespace Advertisements.WebApp.Data.Core.Repositories;

public interface IAdvertisementRepository
{
	IEnumerable<Advertisement> GetAdvertisements(GetAdvertisementsParams getAdvertisementsParams);
	Advertisement GetAdvertisement(int advertisementId, string userId);
	void AddAdvertisement(Advertisement advertisement);
	void UpdateAdvertisement(Advertisement advertisement);
	void FinishAdvertisement(int advertisementId, string userId);
	void DeleteAdvertisement(int advertisementId, string userId);
}