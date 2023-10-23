using System.Collections.Generic;
using System.Linq;
using InvoiceManager.WebApp.NetFramework.Models.Domains;

namespace InvoiceManager.WebApp.NetFramework.Models.Repositories
{
	public class ClientRepository
	{
		internal List<Client> GetClients(string userId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return context.Clients.Where(x => x.UserId == userId).ToList();
			}
		}
	}
}