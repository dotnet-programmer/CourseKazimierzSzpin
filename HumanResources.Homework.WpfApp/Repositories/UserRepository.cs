using HumanResources.Homework.WpfApp.Models;
using HumanResources.Homework.WpfApp.Models.Converters;
using HumanResources.Homework.WpfApp.Models.Wrappers;

namespace HumanResources.Homework.WpfApp.Repositories;

internal class UserRepository
{
	public List<UserWrapper> GetUsers()
	{
		using (AppDbContext context = new())
		{
			return context.Users
				.ToList()
				.Select(x => x.ToWrapper())
				.ToList();
		}
	}
}