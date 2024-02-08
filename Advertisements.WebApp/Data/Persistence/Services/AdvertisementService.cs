using Advertisements.WebApp.Data.Core;
using Advertisements.WebApp.Data.Core.Models;
using Advertisements.WebApp.Data.Core.Models.Domains;
using Advertisements.WebApp.Data.Core.Services;

namespace Advertisements.WebApp.Data.Persistence.Services;

public class AdvertisementService : IAdvertisementService
{
	private readonly IUnitOfWork _unitOfWork;

	public AdvertisementService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

	public IEnumerable<Advertisement> GetAdvertisements(GetAdvertisementsParams getAdvertisementsParams) => 
		_unitOfWork.AdvertisementRepository.GetAdvertisements(getAdvertisementsParams);

	public Advertisement GetAdvertisement(int advertisementId, string userId) => _unitOfWork.AdvertisementRepository.GetAdvertisement(advertisementId, userId);

	public void AddAdvertisement(Advertisement advertisement)
	{
		_unitOfWork.AdvertisementRepository.AddAdvertisement(advertisement);
		_unitOfWork.Complete();
	}

	public void UpdateAdvertisement(Advertisement advertisement)
	{
		_unitOfWork.AdvertisementRepository.UpdateAdvertisement(advertisement);
		_unitOfWork.Complete();
	}

	public void FinishAdvertisement(int advertisementId, string userId)
	{
		_unitOfWork.AdvertisementRepository.FinishAdvertisement(advertisementId, userId);
		_unitOfWork.Complete();
	}

	public void DeleteAdvertisement(int advertisementId, string userId)
	{
		_unitOfWork.AdvertisementRepository.DeleteAdvertisement(advertisementId, userId);
		_unitOfWork.Complete();
	}
}