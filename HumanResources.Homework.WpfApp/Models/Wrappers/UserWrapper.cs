using System.ComponentModel;

namespace HumanResources.Homework.WpfApp.Models.Wrappers;

public class UserWrapper : IDataErrorInfo
{
	public int UserId { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }

	public bool IsValid
		=> string.IsNullOrWhiteSpace(Error);

	#region IDataErrorInfo Members

	public string Error { get; private set; }

	public string this[string columnName]
		=> columnName switch
		{
			nameof(Name) => Error = string.IsNullOrWhiteSpace(Name) ? "Pole Login jest wymagane." : string.Empty,
			_ => string.Empty
		};

	#endregion IDataErrorInfo Members
}