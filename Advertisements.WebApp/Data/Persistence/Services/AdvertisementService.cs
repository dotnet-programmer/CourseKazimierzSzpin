using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Models;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Services;

namespace Advertisements.WebApp.Data.Persistence.Services;

public class AdvertisementService(IUnitOfWork unitOfWork) : IAdvertisementService
{
	public IEnumerable<Advertisement> GetAdvertisements(GetAdvertisementsParams getAdvertisementsParams)
		=> unitOfWork.AdvertisementRepository.GetAdvertisements(getAdvertisementsParams);

	public Advertisement GetAdvertisement(int advertisementId, string userId)
		=> unitOfWork.AdvertisementRepository.GetAdvertisement(advertisementId, userId);

	public void AddAdvertisement(Advertisement advertisement)
	{
		unitOfWork.AdvertisementRepository.AddAdvertisement(advertisement);
		unitOfWork.Complete();
	}

	public void UpdateAdvertisement(Advertisement advertisement)
	{
		unitOfWork.AdvertisementRepository.UpdateAdvertisement(advertisement);
		unitOfWork.Complete();
	}

	public void FinishAdvertisement(int advertisementId, string userId)
	{
		unitOfWork.AdvertisementRepository.FinishAdvertisement(advertisementId, userId);
		unitOfWork.Complete();
	}

	public void DeleteAdvertisement(int advertisementId, string userId)
	{
		unitOfWork.AdvertisementRepository.DeleteAdvertisement(advertisementId, userId);
		unitOfWork.Complete();
	}
}