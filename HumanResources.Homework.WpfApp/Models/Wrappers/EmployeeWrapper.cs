using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HumanResources.Homework.WpfApp.Models.Wrappers;

public class EmployeeWrapper : INotifyDataErrorInfo
{
	private const string ONLY_LETTERS_WARNING = "Pole może zawierać tylko litery.";
	private const string SHORT_TEXT_WARNING = "Pole musi zawierać conajmniej 3 znaki.";
	private const string HIRE_DATE_ERROR = "Data zatrudnienia nie może być późniejsza niż data zwolnienia.";
	private const string FIRE_DATE_ERROR = "Data zwolnienia nie może być wcześniejsza niż data zatrudnienia.";

	private string _firstName;
	private string _lastName;
	private DateTime _hireDate;
	private DateTime? _fireDate;

	/// <summary>
	/// The key is for the property name, the value is the error list for the property
	/// </summary>
	private readonly Dictionary<string, List<string>> _errors = [];

	public EmployeeWrapper()
	{
		FirstName = string.Empty;
		LastName = string.Empty;
		HireDate = DateTime.Now.Date;
		FireDate = null;
	}

	public int EmployeeId { get; set; }

	public string FirstName
	{
		get => _firstName;
		set { _firstName = value; IsNameOrLastNameValid(value, "Imię"); }
	}

	public string LastName
	{
		get => _lastName;
		set { _lastName = value; IsNameOrLastNameValid(value, "Nazwisko"); }
	}

	public string Position { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public decimal? Salary { get; set; }
	public WorkTimeWrapper WorkTime { get; set; } = new();

	public DateTime HireDate
	{
		get => _hireDate;
		set { _hireDate = value; IsHireDateValid(value); }
	}

	public DateTime? FireDate
	{
		get => _fireDate;
		set { _fireDate = value; IsFireDateValid(value); }
	}

	public string Address { get; set; }
	public string Comments { get; set; }

	public bool IsValid 
		=> !HasErrors && WorkTime.IsValid;

	/// <summary>
	/// Validates the FirstName and LastName property, updating the errors collection as needed.
	/// </summary>
	/// <param name="value">Value to validate</param>
	/// <param name="requiredField">Displayed name of property field</param>
	/// <param name="propName">Property name</param>
	private void IsNameOrLastNameValid(string value, string requiredField, [CallerMemberName] string propName = null)
	{
		string requiredFieldErrorMessage = GetRequiredFieldErrorMessage(requiredField);

		if (string.IsNullOrWhiteSpace(value))
		{
			AddError(propName, requiredFieldErrorMessage);
		}
		else
		{
			RemoveErrorOrWarning(propName, requiredFieldErrorMessage);
		}

		if (value.Length < 3)
		{
			AddWarning(propName, SHORT_TEXT_WARNING);
		}
		else
		{
			RemoveErrorOrWarning(propName, SHORT_TEXT_WARNING);
		}

		if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
		{
			AddWarning(propName, ONLY_LETTERS_WARNING);
		}
		else
		{
			RemoveErrorOrWarning(propName, ONLY_LETTERS_WARNING);
		}
	}

	/// <summary>
	/// Validates the HireDate property, updating the errors collection as needed.
	/// </summary>
	/// <param name="value">Value to validate</param>
	/// <param name="propName">Property name</param>
	private void IsHireDateValid(DateTime value, [CallerMemberName] string propName = null)
	{
		if (FireDate != null && value > FireDate)
		{
			AddError(propName, HIRE_DATE_ERROR);
		}
		else
		{
			RemoveErrorOrWarning(propName, HIRE_DATE_ERROR);
		}
	}

	/// <summary>
	/// Validates the FireDate property, updating the errors collection as needed.
	/// </summary>
	/// <param name="value">Value to validate</param>
	/// <param name="propName">Property name</param>
	private void IsFireDateValid(DateTime? value, [CallerMemberName] string propName = null)
	{
		if (value != null && value < HireDate)
		{
			AddError(propName, FIRE_DATE_ERROR);
		}
		else
		{
			RemoveErrorOrWarning(propName, FIRE_DATE_ERROR);
		}
	}

	/// <summary>
	/// Adds the specified error to the errors collection if it is not already present, inserting it in the first position if isWarning is false.
	/// Raises the ErrorsChanged event if the collection changes.
	/// </summary>
	/// <param name="propertyName">Property name</param>
	/// <param name="error">The content of the error</param>
	/// <param name="isError">Specifies whether an error or a warning</param>
	private void AddError(string propertyName, string error, bool isError = true)
	{
		if (!_errors.TryGetValue(propertyName, out List<string> listOfErrors))
		{
			listOfErrors = [];
			_errors[propertyName] = listOfErrors;
		}

		if (!listOfErrors.Contains(error))
		{
			if (isError)
			{
				listOfErrors.Insert(0, error);
			}
			else
			{
				listOfErrors.Add(error);
			}
			OnErrorsChanged(propertyName);
		}
	}

	private void AddWarning(string propertyName, string warning) 
		=> AddError(propertyName, warning, false);

	/// <summary>
	/// Removes the specified error from the errors collection if it is present.
	/// Raises the ErrorsChanged event if the collection changes.
	/// </summary>
	/// <param name="propertyName">Property name</param>
	/// <param name="error">The content of the error</param>
	private void RemoveErrorOrWarning(string propertyName, string error)
	{
		if (_errors.TryGetValue(propertyName, out List<string> listOfErrors) && listOfErrors.Contains(error))
		{
			listOfErrors.Remove(error);
			if (listOfErrors.Count == 0)
			{
				_errors.Remove(propertyName);
			}
			OnErrorsChanged(propertyName);
		}
	}

	/// <summary>
	/// Raises the ErrorsChanged event
	/// </summary>
	/// <param name="propertyName">Property name</param>
	private void OnErrorsChanged(string propertyName) 
		=> ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

	/// <summary>
	/// Get required field error message
	/// </summary>
	/// <param name="fieldName">Name to be used in the error message</param>
	/// <returns>Error message about a required field with the field name inserted</returns>
	private static string GetRequiredFieldErrorMessage(string fieldName) 
		=> $"Pole {fieldName} jest wymagane.";

	#region INotifyDataErrorInfo Members

	public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

	public bool HasErrors 
		=> _errors.Count != 0;

	public IEnumerable GetErrors(string propertyName) 
		=> (string.IsNullOrWhiteSpace(propertyName) || !_errors.ContainsKey(propertyName)) ? null : _errors[propertyName];

	#endregion INotifyDataErrorInfo Members
}