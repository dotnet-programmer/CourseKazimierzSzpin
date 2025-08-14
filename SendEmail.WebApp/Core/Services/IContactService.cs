namespace SendEmail.WebApp.Core.Services;

public interface IContactService
{
	Task<int> GetContactCountAsync(string userId);
	Task RemoveContactAsync(int id, string userId);
}