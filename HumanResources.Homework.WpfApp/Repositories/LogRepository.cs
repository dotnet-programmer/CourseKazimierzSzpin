using HumanResources.Homework.WpfApp.Models;
using HumanResources.Homework.WpfApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Homework.WpfApp.Repositories;

internal class LogRepository
{
	public static async Task LogErrorAsync(string user, Exception ex)
	{
		Log logToSave = new()
		{
			Logged = DateTime.Now,
			User = user,
			Exception = ex.ToString(),
		};

		using (AppDbContext context = new())
		{
			context.Logs.Add(logToSave);
			await context.SaveChangesAsync();
		}
	}

	internal static IEnumerable<Log> GetErrors()
	{
		using (AppDbContext context = new())
		{
			return context.Logs.ToList();
		}
	}

	internal static async Task<IEnumerable<Log>> GetErrorsAsync()
	{
		using (AppDbContext context = new())
		{
			return await context.Logs.ToListAsync();
		}
	}
}