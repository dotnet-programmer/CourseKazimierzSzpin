using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using HumanResources.Homework.WpfApp.Commands;
using HumanResources.Homework.WpfApp.Models;
using HumanResources.Homework.WpfApp.Models.Wrappers;
using HumanResources.Homework.WpfApp.Repositories;
using HumanResources.Homework.WpfApp.Views;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal class MainViewModel : BaseViewModel
{
	private readonly EmployeeRepository _employeeRepository = new();
	private readonly WorkTimeRepository _workTimeRepository = new();

	private ObservableCollection<EmployeeWrapper> _employees;
	private EmployeeWrapper _selectedEmployee;
	private ObservableCollection<WorkTimeWrapper> _workTimes;
	private int _selectedWorkTimeId;
	private ObservableCollection<Employment> _employments;
	private Employment _selectedEmployment;

	public MainViewModel()
	{
		SetCommands();
		SetEmployments();
		SetWorkTimes();
		RefreshEmployees(null);
	}

	public ObservableCollection<EmployeeWrapper> Employees
	{
		get => _employees;
		set { _employees = value; OnPropertyChanged(); }
	}

	public EmployeeWrapper SelectedEmployee
	{
		get => _selectedEmployee;
		set { _selectedEmployee = value; OnPropertyChanged(); }
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

	public ObservableCollection<Employment> Employments
	{
		get => _employments;
		set { _employments = value; OnPropertyChanged(); }
	}
	public Employment SelectedEmployment
	{
		get => _selectedEmployment;
		set { _selectedEmployment = value; OnPropertyChanged(); }
	}

	public ICommand AddEmployeeCommand { get; private set; }
	public ICommand EditEmployeeCommand { get; private set; }
	public ICommand FireEmployeeCommand { get; private set; }
	public ICommand RefreshEmployeesCommand { get; private set; }
	public ICommand SelectionChangedCommand { get; private set; }
	public ICommand LogoutCommand { get; private set; }
	public ICommand GetErrorsCommand { get; private set; }

	private void SetCommands()
	{
		AddEmployeeCommand = new RelayCommand(AddEditEmployee);
		EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditEmployee);
		FireEmployeeCommand = new RelayCommand(FireEmployee, CanFireEmployee);
		RefreshEmployeesCommand = new RelayCommand(RefreshEmployees);
		SelectionChangedCommand = new RelayCommand(SelectionChanged);
		LogoutCommand = new RelayCommand(Logout);
		GetErrorsCommand = new RelayCommand(GetErrors);
	}

	private void AddEditEmployee(object commandParameter)
	{
        if (commandParameter != null)
        {
			ShowAddEditEmployeeView((EmployeeWrapper)commandParameter);
		}
        else
		{
			ShowAddEditEmployeeView();
		}
	}

	private bool CanEditEmployee(object commandParameter) 
		=> SelectedEmployee != null;

	private void FireEmployee(object commandParameter) 
		=> ShowAddEditEmployeeView((EmployeeWrapper)commandParameter, fireTheEmployee: true);

	private void ShowAddEditEmployeeView(EmployeeWrapper employeeWrapper = null, bool fireTheEmployee = false)
	{
		AddEditEmployeeView addEditEmployeeView = new(employeeWrapper, fireTheEmployee);
		addEditEmployeeView.ShowDialog();
		RefreshEmployees(null);
	}

	private bool CanFireEmployee(object commandParameter) 
		=> SelectedEmployee != null && SelectedEmployee.FireDate == null;

	private void RefreshEmployees(object commandParameter) 
		=> Employees = new(_employeeRepository.GetEmployees(SelectedWorkTimeId, SelectedEmployment));

	private void SelectionChanged(object commandParameter) 
		=> RefreshEmployees(null);

	private void Logout(object commandParameter)
	{
		Process.Start(Application.ResourceAssembly.Location.Replace(".dll", ".exe"));
		Application.Current.Shutdown();
	}

	private void GetErrors(object commandParameter)
	{
		ErrorsView errorsView = new();
		errorsView.ShowDialog();
	}

	private void SetEmployments() 
		=> Employments = new(Enum.GetValues<Employment>());

	private void SetWorkTimes()
	{
		var workTimes = _workTimeRepository.GetWorkTimes();
		workTimes.Insert(0, new WorkTimeWrapper { WorkTimeId = 0, WorkTimeName = "Wszyscy" });
		WorkTimes = new ObservableCollection<WorkTimeWrapper>(workTimes);
		SelectedWorkTimeId = 0;
	}
}