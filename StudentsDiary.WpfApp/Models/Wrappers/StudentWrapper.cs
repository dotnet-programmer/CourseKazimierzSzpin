using System.ComponentModel;

namespace StudentsDiary.WpfApp.Models.Wrappers;

public class StudentWrapper : IDataErrorInfo
{
	private bool _isFirstNameValid;
	private bool _isLastNameValid;

	public StudentWrapper() 
		=> Group = new();

	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Comments { get; set; }
	public string Math { get; set; }
	public string Technology { get; set; }
	public string Physics { get; set; }
	public string PolishLang { get; set; }
	public string ForeignLang { get; set; }
	public bool Activities { get; set; }
	public GroupWrapper Group { get; set; }

	public bool IsValid
		=> _isFirstNameValid && _isLastNameValid && Group.IsValid;

	public string Error { get; set; }

	public string this[string columnName]
	{
		get
		{
			switch (columnName)
			{
				case nameof(FirstName):
					_isFirstNameValid = !string.IsNullOrWhiteSpace(FirstName);
					Error = _isFirstNameValid ? string.Empty : "Pole Imię jest wymagane.";
					break;
				case nameof(LastName):
					if (string.IsNullOrWhiteSpace(LastName))
					{
						Error = "Pole Nazwisko jest wymagane.";
						_isLastNameValid = false;
					}
					else
					{
						Error = string.Empty;
						_isLastNameValid = true;
					}
					break;
				default:
					break;
			}
			return Error;
		}
	}
}