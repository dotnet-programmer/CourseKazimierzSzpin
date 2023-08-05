using HumanResources.Homework.WpfApp.Models.Domains;
using HumanResources.Homework.WpfApp.Models.Wrappers;

namespace HumanResources.Homework.WpfApp.Models.Converters;

public static class UserConverter
{
	public static UserWrapper ToWrapper(this User user) => new()
	{
		UserId = user.UserId,
		Name = user.Name,
		Password = user.Password,
	};

	public static User ToDao(this UserWrapper userWrapper) => new()
	{
		UserId = userWrapper.UserId,
		Name = userWrapper.Name,
		Password = userWrapper.Password,
	};
}