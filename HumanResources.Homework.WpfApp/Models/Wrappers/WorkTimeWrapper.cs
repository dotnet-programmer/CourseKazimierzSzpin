using System.ComponentModel;

namespace HumanResources.Homework.WpfApp.Models.Wrappers;

public class WorkTimeWrapper : IDataErrorInfo
{
	public int WorkTimeId { get; set; }
	public string WorkTimeName { get; set; }

	#region IDataErrorInfo Members

	public bool IsValid => string.IsNullOrWhiteSpace(Error);
	public string Error { get; private set; }

	public string this[string columnName] => columnName switch
	{
		nameof(WorkTimeId) => Error = WorkTimeId == 0 ? "Pole Wymiar pracy jest wymagane." : string.Empty
	};

	#endregion IDataErrorInfo Members
}