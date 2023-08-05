using System.Collections.ObjectModel;
using System.Windows.Input;
using HumanResources.Homework.WpfApp.Commands;
using HumanResources.Homework.WpfApp.Models.Wrappers;
using HumanResources.Homework.WpfApp.Repositories;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal class AddEditEmployeeViewModel : BaseViewModel
{
	public AddEditEmployeeViewModel(EmployeeWrapper employeeWrapper = null, bool fireTheEmployee = false)
	{
		if (employeeWrapper == null)
		{
			Employee = new EmployeeWrapper
			{
				HireDate = DateTime.Now.Date,
				FireDate = null
			};
		}
		else
		{
			Employee = employeeWrapper;
			IsUpdate = true;
			IsFireDateVisible = Employee.FireDate != null || fireTheEmployee;
			IsFireTheEmployee = fireTheEmployee;
			if (fireTheEmployee)
			{
				Employee.FireDate = DateTime.Now.Date;
			}
		}

		SetCommands();
		SetWorkTimes();
	}

	#region Property binding

	private EmployeeWrapper _employeeWrapper;
	public EmployeeWrapper Employee
	{
		get => _employeeWrapper;
		set { _employeeWrapper = value; OnPropertyChanged(); }
	}

	private bool _isUpdate;
	public bool IsUpdate
	{
		get => _isUpdate;
		set { _isUpdate = value; OnPropertyChanged(); }
	}

	private bool _isFireDateVisible;
	public bool IsFireDateVisible
	{
		get => _isFireDateVisible;
		set { _isFireDateVisible = value; OnPropertyChanged(); }
	}

	private bool _isFireTheEmployee;
	public bool IsFireTheEmployee
	{
		get => !_isFireTheEmployee;
		set { _isFireTheEmployee = value; OnPropertyChanged(); }
	}

	private ObservableCollection<WorkTimeWrapper> _workTimes;
	public ObservableCollection<WorkTimeWrapper> WorkTimes
	{
		get => _workTimes;
		set { _workTimes = value; OnPropertyChanged(); }
	}

	private int _selectedWorkTimeId;
	public int SelectedWorkTimeId
	{
		get => _selectedWorkTimeId;
		set { _selectedWorkTimeId = value; OnPropertyChanged(); }
	}

	#endregion Property binding

	public ICommand ConfirmCommand { get; private set; }

	private void SetCommands()
	{
		ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
		//ConfirmCommand = new RelayCommandAsync(ConfirmAsync, CanConfirm);
	}

	private void Confirm(object commandParameter)
	{
		if (!IsUpdate)
		{
			AddEmployee();
		}
		else
		{
			UpdateEmployee();
		}
	}

	//private async Task ConfirmAsync(object commandParameter)
	//{
	//	if (!IsUpdate)
	//	{
	//		await AddEmployeeAsync();
	//	}
	//	else
	//	{
	//		await UpdateEmployeeAsync();
	//	}
	//}

	private bool CanConfirm(object commandParameter) => Employee.IsValid;

	private void SetWorkTimes()
	{
		var workTimes = WorkTimeRepository.GetWorkTimes();
		workTimes.Insert(0, new WorkTimeWrapper { WorkTimeId = 0, WorkTimeName = "-- brak --" });
		WorkTimes = new ObservableCollection<WorkTimeWrapper>(workTimes);
		SelectedWorkTimeId = Employee.WorkTime.WorkTimeId;
	}

	private void AddEmployee() => EmployeeRepository.AddEmployee(Employee);

	private void UpdateEmployee() => EmployeeRepository.UpdateEmployee(Employee);

	//private async Task AddEmployeeAsync() => await EmployeeRepository.AddEmployeeAsync(Employee);

	//private async Task UpdateEmployeeAsync() => await EmployeeRepository.UpdateEmployeeAsync(Employee);

}