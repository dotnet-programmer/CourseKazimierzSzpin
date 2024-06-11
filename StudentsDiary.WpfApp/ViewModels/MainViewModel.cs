using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using StudentsDiary.WpfApp.Commands;
using StudentsDiary.WpfApp.Models;
using StudentsDiary.WpfApp.Models.Domains;
using StudentsDiary.WpfApp.Models.Wrappers;
using StudentsDiary.WpfApp.Repositories;
using StudentsDiary.WpfApp.Views;

namespace StudentsDiary.WpfApp.ViewModels;

internal class MainViewModel : BaseViewModel
{
	// INFO - MahApps 6 - dodanie okien dialogowych zgodnych z MVVM
	private readonly IDialogCoordinator _dialogCoordinator;

	private readonly GroupRepository _groupRepository = new();
	private readonly StudentRepository _studentRepository = new();

	public MainViewModel(IDialogCoordinator instance)
	{
		// INFO - MahApps 6 - dodanie okien dialogowych zgodnych z MVVM
		_dialogCoordinator = instance;
		SetCommands();
	}

	#region Property binding

	// INFO - Bindowanie właściwości - pełna właściwość (pole + prop), w set dodać OnPropertyChanged()

	private ObservableCollection<StudentWrapper> _students;
	public ObservableCollection<StudentWrapper> Students
	{
		get => _students;
		set { _students = value; OnPropertyChanged(); }
	}

	private StudentWrapper _selectedStudent;
	public StudentWrapper SelectedStudent
	{
		get => _selectedStudent;
		set { _selectedStudent = value; OnPropertyChanged(); }
	}

	private ObservableCollection<Group> _groups;
	public ObservableCollection<Group> Groups
	{
		get => _groups;
		set { _groups = value; OnPropertyChanged(); }
	}

	private int _selectedGroupId;
	public int SelectedGroupId
	{
		get => _selectedGroupId;
		set { _selectedGroupId = value; OnPropertyChanged(); }
	}

	#endregion Property binding

	#region Command binding

	// INFO - Bindowanie metod - właściwość ICommand + new RelayCommand(wymagana metoda, opcjonalna metoda)

	public ICommand AddStudentCommand { get; private set; }
	public ICommand EditStudentCommand { get; private set; }
	public ICommand DeleteStudentCommand { get; private set; }
	public ICommand RefreshStudentsCommand { get; private set; }
	public ICommand ShowSettingsCommand { get; private set; }
	public ICommand LoadedWindowCommand { get; private set; }
	public ICommand SetColumnsWidthCommand { get; private set; }
	public ICommand GridDoubleClickCommand { get; private set; }

	// INFO - Bindowanie metod - właściwość ICommand + new RelayCommand(wymagana metoda, opcjonalna metoda)
	private void SetCommands()
	{
		AddStudentCommand = new RelayCommand(AddEditStudent);
		EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
		DeleteStudentCommand = new RelayCommandAsync(DeleteStudentAsync, CanEditDeleteStudent);
		RefreshStudentsCommand = new RelayCommand(RefreshStudents);
		ShowSettingsCommand = new RelayCommand(ShowSettings);
		LoadedWindowCommand = new RelayCommand(LoadedWindow);
		SetColumnsWidthCommand = new RelayCommand(SetColumnsWidth);
		GridDoubleClickCommand = new RelayCommand(GridDoubleClick, CanEditDeleteStudent);
	}

	private void AddEditStudent(object commandParameter)
	{
		// INFO - MVVM otwieranie okna 1
		// to nie jest dobra praktyka żeby wywoływać nowe okno z ViewModelu
		// takie rozwiązanie może powodować problemy z testami jednostkowymi
		// lepiej jest zrobić przez Dependency Injection
		AddEditStudentView addEditStudentView = new(commandParameter as StudentWrapper);
		addEditStudentView.Closed += AddEditStudentView_Closed;
		addEditStudentView.ShowDialog();
	}

	private void AddEditStudentView_Closed(object sender, EventArgs e)
		=> RefreshDiary();

	private bool CanEditDeleteStudent(object commandParameter)
		=> SelectedStudent != null;

	private async Task DeleteStudentAsync(object commandParameter)
	{
		// INFO - MahApps 7 - dodanie okien dialogowych
		//var metroWindow = Application.Current.MainWindow as MetroWindow;
		//var dialogResult = await metroWindow.ShowMessageAsync(
		//	"Usuwanie ucznia",
		//	$"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
		//	MessageDialogStyle.AffirmativeAndNegative);

		var dialogResult = await ShowMessage(
			"Usuwanie ucznia",
			$"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
			MessageDialogStyle.AffirmativeAndNegative);

		if (dialogResult == MessageDialogResult.Affirmative)
		{
			_studentRepository.DeleteStudent(SelectedStudent.Id);
			RefreshDiary();
		}
	}

	private void RefreshStudents(object commandParameter)
		=> RefreshDiary();

	private void ShowSettings(object commandParameter)
	{
		SettingsView settingsView = new(true);
		settingsView.ShowDialog();
	}

	private async void LoadedWindow(object commandParameter)
	{
		if (!IsValidConnectionToDataBase())
		{
			//var metroWindow = Application.Current.MainWindow as MetroWindow;
			//var dialogResult = await metroWindow.ShowMessageAsync("Błąd połączenia", $"Nie można połączyć się z bazą danych.{Environment.NewLine}Czy chcesz zmienić ustawienia?", MessageDialogStyle.AffirmativeAndNegative);

			var dialogResult = await ShowMessage(
				"Błąd połączenia",
				$"Nie można połączyć się z bazą danych.{Environment.NewLine}Czy chcesz zmienić ustawienia?",
				MessageDialogStyle.AffirmativeAndNegative);

			if (dialogResult == MessageDialogResult.Negative)
			{
				Application.Current.Shutdown();
			}
			else
			{
				SettingsView settingsWindow = new(false);
				settingsWindow.ShowDialog();
			}
		}
		else
		{
			RefreshDiary();
			SetGroups();
		}
	}

	private static bool IsValidConnectionToDataBase()
	{
		try
		{
			using (AppDbContext context = new())
			{
				context.Database.OpenConnection();
				context.Database.CloseConnection();
			}
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	private void SetColumnsWidth(object commandParameter)
	{
		if (commandParameter is DataGrid dataGrid)
		{
			foreach (var column in dataGrid.Columns)
			{
				column.MinWidth = column.ActualWidth;
				column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}
		}
	}

	private void GridDoubleClick(object commandParameter)
		=> AddEditStudent(commandParameter);

	private void SetGroups()
	{
		var groups = _groupRepository.GetGroups();
		groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });
		Groups = new ObservableCollection<Group>(groups);
		SelectedGroupId = 0;
	}

	private void RefreshDiary()
		=> Students = new ObservableCollection<StudentWrapper>(_studentRepository.GetStudents(SelectedGroupId));

	#endregion Command binding

	#region okna dialogowe MahApps

	// INFO - MahApps 6 - dodanie okien dialogowych zgodnych z MVVM

	// Simple method which can be used on a Button
	private async void FooMessage()
		=> await _dialogCoordinator.ShowMessageAsync(this, "Message Title", "Bar");

	private async Task<MessageDialogResult> ShowMessage(
		string title,
		string message,
		MessageDialogStyle dialogStyle = MessageDialogStyle.Affirmative,
		MetroDialogSettings dialogSettings = null)
		=> await _dialogCoordinator.ShowMessageAsync(this, title, message, dialogStyle, dialogSettings);

	private async void FooProgress()
	{
		// Show...
		ProgressDialogController controller = await _dialogCoordinator.ShowProgressAsync(this, "Wait", "Waiting for the Answer to the Ultimate Question of Life, The Universe, and Everything...");

		controller.SetIndeterminate();

		// Do your work...
		//var result = await Task.Run();

		// Close...
		await controller.CloseAsync();
	}

	#endregion okna dialogowe MahApps
}