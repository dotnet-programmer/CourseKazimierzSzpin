using HumanResources.Homework.WpfApp.Models.Domains;
using HumanResources.Homework.WpfApp.Models.Wrappers;

namespace HumanResources.Homework.WpfApp.Models.Converters;

public static class WorkTimeConverter
{
	public static WorkTimeWrapper ToWrapper(this WorkTime workTime)
		=> new()
		{
			WorkTimeId = workTime.WorkTimeId,
			WorkTimeName = workTime.WorkTimeName,
		};

	public static WorkTime ToDao(this WorkTimeWrapper workTimeWrapper)
		=> new()
		{
			WorkTimeId = workTimeWrapper.WorkTimeId,
			WorkTimeName = workTimeWrapper.WorkTimeName,
		};
}