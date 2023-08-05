using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.Homework.WpfApp.Models.Wrappers;

public class EmployeeWrapper : IDataErrorInfo
//public class EmployeeWrapper : INotifyDataErrorInfo 
{
	private bool _isFirstNameValid;
	private bool _isLastNameValid;
	private bool _isFireDateValid;
	private bool _isHireDateValid;

	/// <summary>
	/// The key is for the property name, the value is the error list for the property
	/// </summary>
	//private readonly Dictionary<String, List<String>> _errors = new();

	public int EmployeeId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Position { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public decimal? Salary { get; set; }
	public WorkTimeWrapper WorkTime { get; set; } = new();
	public DateTime HireDate { get; set; }
	public DateTime? FireDate { get; set; }
	public string Address { get; set; }
	public string Comments { get; set; }

	//#region INotifyDataErrorInfo Members

	//public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

	//public bool HasErrors => _errors.Any();

	//public IEnumerable GetErrors(string? propertyName) => (string.IsNullOrWhiteSpace(propertyName) || !_errors.ContainsKey(propertyName)) ? null : _errors[propertyName];


	//#endregion INotifyDataErrorInfo Members

	///// <summary>
	///// Adds the specified error to the errors collection if it is not already present, inserting it in the first position if isWarning is false.
	///// Raises the ErrorsChanged event if the collection changes. 
	///// </summary>
	///// <param name="propertyName"></param>
	///// <param name="error"></param>
	///// <param name="isWarning"></param>
	//public void AddError(string propertyName, string error, bool isWarning)
	//{
	//	if (!_errors.ContainsKey(propertyName))
	//	{
	//		_errors[propertyName] = new List<string>();
	//	}

	//	if (!_errors[propertyName].Contains(error))
	//	{
	//		if (isWarning)
	//		{
	//			_errors[propertyName].Add(error);
	//		}
	//		else
	//		{
	//			_errors[propertyName].Insert(0, error);
	//		}
	//		OnErrorsChanged(propertyName);
	//	}
	//}

	///// <summary>
	///// Removes the specified error from the errors collection if it is present.
	///// Raises the ErrorsChanged event if the collection changes.
	///// </summary>
	///// <param name="propertyName"></param>
	///// <param name="error"></param>
	//public void RemoveError(string propertyName, string error)
	//{
	//	if (_errors.ContainsKey(propertyName) && _errors[propertyName].Contains(error))
	//	{
	//		_errors[propertyName].Remove(error);
	//		if (_errors[propertyName].Count == 0)
	//		{
	//			_errors.Remove(propertyName);
	//		}
	//		OnErrorsChanged(propertyName);
	//	}
	//}

	//private void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

	#region IDataErrorInfo Members

	public bool IsValid => _isFirstNameValid && _isLastNameValid && _isHireDateValid && _isFireDateValid && WorkTime.IsValid;
	public string Error { get; private set; }

	public string this[string columnName]
	{
		get
		{
			switch (columnName)
			{
				case nameof(FirstName):
					_isFirstNameValid = !string.IsNullOrWhiteSpace(FirstName);
					Error = GetRequiredFieldErrorMessage(_isFirstNameValid, "Imię");
					//Error = _isFirstNameValid ? string.Empty : "Pole Imię jest wymagane.";
					break;
				case nameof(LastName):
					_isLastNameValid = !string.IsNullOrWhiteSpace(LastName);
					Error = GetRequiredFieldErrorMessage(_isLastNameValid, "Nazwisko");
					//Error = _isLastNameValid ? string.Empty : "Pole Nazwisko jest wymagane.";
					break;
				case nameof(HireDate):
					_isHireDateValid = (FireDate is null || HireDate <= FireDate);
					//Error = GetErrorMessage(_isHireDateValid, "Data zatrudnienia nie może być późniejsza niż data zwolnienia.");
					Error = _isHireDateValid ? string.Empty : "Data zatrudnienia nie może być późniejsza niż data zwolnienia.";
					break;
				case nameof(FireDate):
					_isFireDateValid = (FireDate is null || FireDate >= HireDate);
					//Error = GetErrorMessage(_isFireDateValid, "Data zwolnienia nie może być wcześniejsza niż data zatrudnienia.");
					Error = _isFireDateValid ? string.Empty : "Data zwolnienia nie może być wcześniejsza niż data zatrudnienia.";
					break;
			}
			return Error;
		}
	}

	#endregion IDataErrorInfo Members

	private string GetRequiredFieldErrorMessage(bool isValid, string fieldName) => isValid ? string.Empty : $"Pole {fieldName} jest wymagane.";

	//private string GetErrorMessage(bool isValid, string errorMessage) => isValid ? string.Empty : errorMessage;

}