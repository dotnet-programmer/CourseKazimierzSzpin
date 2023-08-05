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
	public MainViewModel()
	{
		SetCommands();
		SetEmployments();
		SetWorkTimes();
		RefreshEmployees(null);
	}

	#region Property binding

	private ObservableCollection<EmployeeWrapper> _employees;
	public ObservableCollection<EmployeeWrapper> Employees
	{
		get => _employees;
		set { _employees = value; OnPropertyChanged(); }
	}

	private EmployeeWrapper _selectedEmployee;
	public EmployeeWrapper SelectedEmployee
	{
		get => _selectedEmployee;
		set { _selectedEmployee = value; OnPropertyChanged(); }
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

	private ObservableCollection<Employment> _employments;
	public ObservableCollection<Employment> Employments
	{
		get => _employments;
		set { _employments = value; OnPropertyChanged(); }
	}

	private Employment _selectedEmployment;
	public Employment SelectedEmployment
	{
		get => _selectedEmployment;
		set { _selectedEmployment = value; OnPropertyChanged(); }
	}

	#endregion Property binding

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

	private void AddEditEmployee(object commandParameter) => ShowAddEditEmployeeView(new((EmployeeWrapper)commandParameter));

	private bool CanEditEmployee(object commandParameter) => SelectedEmployee != null;

	private void FireEmployee(object commandParameter) => ShowAddEditEmployeeView(new((EmployeeWrapper)commandParameter, fireTheEmployee: true));

	private void ShowAddEditEmployeeView(AddEditEmployeeView addEditEmployeeView)
	{
		addEditEmployeeView.ShowDialog();
		RefreshEmployees(null);
	}

	private bool CanFireEmployee(object commandParameter) => SelectedEmployee != null && SelectedEmployee.FireDate == null;

	private void RefreshEmployees(object? commandParameter) => Employees = new(EmployeeRepository.GetEmployees(SelectedWorkTimeId, SelectedEmployment));

	private void SelectionChanged(object commandParameter) => RefreshEmployees(null);

	private void Logout(object commandParameter)
	{
		var appLocation = Application.ResourceAssembly.Location;
		Process.Start(appLocation.EndsWith(".dll") ? appLocation.Replace(".dll", ".exe") : appLocation);
		Application.Current.Shutdown();
	}

	private void GetErrors(object commandParameter)
	{
		ErrorsView errorsView = new();
		errorsView.ShowDialog();
	}

	private void SetEmployments() => Employments = new(Enum.GetValues<Employment>());

	private void SetWorkTimes()
	{
		var workTimes = WorkTimeRepository.GetWorkTimes();
		workTimes.Insert(0, new WorkTimeWrapper { WorkTimeId = 0, WorkTimeName = "Wszyscy" });
		WorkTimes = new ObservableCollection<WorkTimeWrapper>(workTimes);
		SelectedWorkTimeId = 0;
	}
}