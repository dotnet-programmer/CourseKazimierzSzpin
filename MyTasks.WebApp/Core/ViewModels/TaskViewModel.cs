using MyTasks.WebApp.Core.Models.Domains;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Core.ViewModels;

public class TaskViewModel
{
    public string Heading { get; set; }
    public Task	Task{ get; set; }
    public IEnumerable<Category> Categories { get; set; }
}
