using HumanResources.Homework.WpfApp.Models;
using HumanResources.Homework.WpfApp.Models.Converters;
using HumanResources.Homework.WpfApp.Models.Wrappers;

namespace HumanResources.Homework.WpfApp.Repositories;

internal static class WorkTimeRepository
{
	public static List<WorkTimeWrapper> GetWorkTimes()
	{
		using (AppDbContext context = new())
		{
			return context.WorkTimes.ToList().Select(x => x.ToWrapper()).ToList();
		}
	}

	public static async Task AddWorkTimeAsync(WorkTimeWrapper workTimeWrapper)
	{
		var workTime = workTimeWrapper.ToDao();

		using (AppDbContext context = new())
		{
			if (context.WorkTimes.Where(x => x.WorkTimeName == workTime.WorkTimeName).Any())
			{
				return;
			}
			context.WorkTimes.Add(workTime);
			await context.SaveChangesAsync();
		}
	}
}