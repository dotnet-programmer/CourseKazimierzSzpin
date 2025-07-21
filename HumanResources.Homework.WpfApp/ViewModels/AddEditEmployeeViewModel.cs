using System.Collections.ObjectModel;
using System.Windows.Input;
using HumanResources.Homework.WpfApp.Commands;
using HumanResources.Homework.WpfApp.Models.Wrappers;
using HumanResources.Homework.WpfApp.Repositories;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal class AddEditEmployeeViewModel : BaseViewModel
{
	private readonly EmployeeRepository _employeeRepository = new();
	private readonly WorkTimeRepository _workTimeRepository = new();

	private EmployeeWrapper _employeeWrapper;

	private bool _isUpdate;
	private bool _isFireDateVisible;
	private bool _isFireTheEmployee;
	private ObservableCollection<WorkTimeWrapper> _workTimes;
	private int _selectedWorkTimeId;

	public AddEditEmployeeViewModel(EmployeeWrapper employeeWrapper = null, bool fireTheEmployee = false)
	{
		if (employeeWrapper == null)
		{
			Employee = new();
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

	public EmployeeWrapper Employee
	{
		get => _employeeWrapper;
		set { _employeeWrapper = value; OnPropertyChanged(); }
	}

	public bool IsUpdate
	{
		get => _isUpdate;
		set { _isUpdate = value; OnPropertyChanged(); }
	}

	public bool IsFireDateVisible
	{
		get => _isFireDateVisible;
		set { _isFireDateVisible = value; OnPropertyChanged(); }
	}

	public bool IsFireTheEmployee
	{
		get => !_isFireTheEmployee;
		set { _isFireTheEmployee = value; OnPropertyChanged(); }
	}

	public ObservableCollection<WorkTimeWrapper> WorkTimes
	{
		get => _workTimes;
		set { _workTimes = value; OnPropertyChanged(); }
	}

	public int SelectedWorkTimeId
	{
		get => _selectedWorkTimeId;
		set { _selectedWorkTimeId = value; OnPropertyChanged(); }
	}

	public ICommand ConfirmCommand { get; private set; }

	private void SetCommands()
		=> ConfirmCommand = new RelayCommand(Confirm, CanConfirm);

	private void Confirm(object commandParameter)
	{
		if (IsUpdate)
		{
			UpdateEmployee();
		}
		else
		{
			AddEmployee();
		}
	}

	private bool CanConfirm(object commandParameter)
		=> Employee.IsValid;

	private void SetWorkTimes()
	{
		var workTimes = _workTimeRepository.GetWorkTimes();
		workTimes.Insert(0, new WorkTimeWrapper { WorkTimeId = 0, WorkTimeName = "-- brak --" });
		WorkTimes = new(workTimes);
		SelectedWorkTimeId = Employee.WorkTime.WorkTimeId;
	}

	private void AddEmployee()
		=> _employeeRepository.AddEmployee(Employee);

	private void UpdateEmployee()
		=> _employeeRepository.UpdateEmployee(Employee);
}