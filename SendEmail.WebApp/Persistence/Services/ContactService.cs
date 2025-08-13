using SendEmail.WebApp.Core;
using SendEmail.WebApp.Core.Services;

namespace SendEmail.WebApp.Persistence.Services;

public class ContactService(IUnitOfWork unitOfWork) : IContactService
{
	public async Task<int> GetContactCountAsync(string userId)
	{
		if (string.IsNullOrWhiteSpace(userId))
		{
			throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
		}
		return await unitOfWork.ContactRepository.GetContactCountAsync(userId);
	}
}