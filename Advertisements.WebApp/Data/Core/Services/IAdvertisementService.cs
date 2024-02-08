using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Models;

namespace Advertisements.WebApp.Data.Core.Services;

public interface IAdvertisementService
{
	IEnumerable<Advertisement> GetAdvertisements(GetAdvertisementsParams getAdvertisementsParams);

	Advertisement GetAdvertisement(int advertisementId, string userId);

	void AddAdvertisement(Advertisement advertisement);

	void UpdateAdvertisement(Advertisement advertisement);

	void FinishAdvertisement(int advertisementId, string userId);

	void DeleteAdvertisement(int advertisementId, string userId);
}