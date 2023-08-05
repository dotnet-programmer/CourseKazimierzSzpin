using HumanResources.Homework.WpfApp.Models;
using HumanResources.Homework.WpfApp.Models.Converters;
using HumanResources.Homework.WpfApp.Models.Wrappers;

namespace HumanResources.Homework.WpfApp.Repositories;

internal static class UserRepository
{
	public static List<UserWrapper> GetUsers()
	{
		using (AppDbContext context = new())
		{
			return context.Users.ToList().Select(x => x.ToWrapper()).ToList();
		}
	}
}