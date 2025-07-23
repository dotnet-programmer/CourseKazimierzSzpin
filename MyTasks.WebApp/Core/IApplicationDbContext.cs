using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core.Models.Domains;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Core;

public interface IApplicationDbContext
{
	DbSet<Task> Tasks { get; set; }
	DbSet<Category> Categories { get; set; }
	int SaveChanges();
}