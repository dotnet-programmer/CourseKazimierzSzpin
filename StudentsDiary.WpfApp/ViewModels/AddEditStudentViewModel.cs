using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using StudentsDiary.WpfApp.Commands;
using StudentsDiary.WpfApp.Models.Domains;
using StudentsDiary.WpfApp.Models.Wrappers;
using StudentsDiary.WpfApp.Repositories;

namespace StudentsDiary.WpfApp.ViewModels;

internal class AddEditStudentViewModel : BaseViewModel
{
	private readonly GroupRepository _groupRepository = new();
	private readonly StudentRepository _studentRepository = new();

	public AddEditStudentViewModel(StudentWrapper student = null)
	{
		if (student is null)
		{
			Student = new();
		}
		else
		{
			Student = student;
			IsUpdate = true;
		}
		SetCommands();
		SetGroups();
	}

	// INFO - MVVM Zamykanie okna 3 - użycie delegata
	//public Action CloseAction { get; set; }
	//prop albo pole
	//public Action CloseAction;

	// INFO - MVVM Zamykanie okna 4 - użycie eventa
	//public event Action CloseAction;

	#region Property binding

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

	private StudentWrapper _student;
	public StudentWrapper Student
	{
		get => _student;
		set { _student = value; OnPropertyChanged(); }
	}

	private bool _isUpdate;
	public bool IsUpdate
	{
		get => _isUpdate;
		set { _isUpdate = value; OnPropertyChanged(); }
	}

	#endregion Property binding

	#region Command binding

	public ICommand ConfirmCommand { get; private set; }
	public ICommand CloseWindowCommand { get; private set; }

	private void SetCommands()
	{
		ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
		CloseWindowCommand = new RelayCommand(CloseWindow);
	}

	private void Confirm(object commandParameter)
	{
		if (!IsUpdate)
		{
			AddStudent();
		}
		else
		{
			UpdateStudent();
		}

		// INFO - MVVM Zamykanie okna 3 - użycie delegata
		//OnCloseAction();

		// INFO - MVVM Zamykanie okna 4 - użycie eventa
		//OnCloseAction();

		//(commandParameter as Window)?.Close();
		CloseWindow(commandParameter);
	}

	private bool CanConfirm(object commandParameter)
		=> Student.IsValid;

	// INFO - MVVM Zamykanie okna 1 - przekazanie całego okna jako parametr
	private void CloseWindow(object commandParameter)
		=> (commandParameter as Window)?.Close();

	// INFO - MVVM Zamykanie okna 4 - użycie eventa
	//private void OnCloseAction()
	//{
	//	CloseAction?.Invoke();
	//}

	private void AddStudent()
		=> _studentRepository.AddStudent(Student);

	private void UpdateStudent()
		=> _studentRepository.UpdateStudent(Student);

	#endregion Command binding

	private void SetGroups()
	{
		var groups = _groupRepository.GetGroups();
		groups.Insert(0, new Group { Id = 0, Name = "-- brak --" });
		Groups = new ObservableCollection<Group>(groups);
		SelectedGroupId = Student.Group.Id;
	}
}