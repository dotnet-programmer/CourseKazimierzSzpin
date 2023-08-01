using System.ComponentModel;

namespace StudentsDiary.WpfApp.Models.Wrappers;

public class GroupWrapper : IDataErrorInfo
{
	public int Id { get; set; }
	public string Name { get; set; }

	public bool IsValid => string.IsNullOrWhiteSpace(Error);
	public string Error { get; set; }

	public string this[string columnName]
	{
		get
		{
			switch (columnName)
			{
				case nameof(Id):
					Error = Id == 0 ? "Pole Grupa jest wymagane." : string.Empty;
					break;
				default:
					break;
			}
			return Error;
		}
	}
}