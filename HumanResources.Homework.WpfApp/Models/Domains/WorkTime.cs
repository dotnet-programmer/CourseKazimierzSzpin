using System.Collections.ObjectModel;

namespace HumanResources.Homework.WpfApp.Models.Domains;

public class WorkTime
{
	public int WorkTimeId { get; set; }
	public string WorkTimeName { get; set; }

	// ****************************************************************

	public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
}