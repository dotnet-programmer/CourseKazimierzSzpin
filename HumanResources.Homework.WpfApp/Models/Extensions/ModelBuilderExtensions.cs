using HumanResources.Homework.WpfApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Homework.WpfApp.Models.Extensions;

public static class ModelBuilderExtensions
{
	public static void SeedUsers(this ModelBuilder modelBuilder)
		=> modelBuilder.Entity<User>().HasData(
			new User { UserId = 1, Name = "admin", Password = StringCipherHelper.EncryptString("admin") },
			new User { UserId = 2, Name = "user1", Password = StringCipherHelper.EncryptString("user1") }
			);

	public static void SeedWorkTimes(this ModelBuilder modelBuilder)
		=> modelBuilder.Entity<WorkTime>().HasData(
			new WorkTime { WorkTimeId = 1, WorkTimeName = "Full-Time" },
			new WorkTime { WorkTimeId = 2, WorkTimeName = "Part-Time" },
			new WorkTime { WorkTimeId = 3, WorkTimeName = "Freelance" }
			);
}