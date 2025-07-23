using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;

namespace InvoiceManager.WebApp.Persistence.Repositories;

public class ClientRepository(IApplicationDbContext context)
{
	internal List<Client> GetClients(string userId)
		=> context.Clients.Where(x => x.UserId == userId).ToList();
}