using MahApps.Metro.Controls;
using StudentsDiary.WpfApp.Models.Wrappers;
using StudentsDiary.WpfApp.ViewModels;

namespace StudentsDiary.WpfApp.Views;

/// <summary>
/// Interaction logic for AddEditStudentView.xaml
/// </summary>
public partial class AddEditStudentView : MetroWindow
{
	public AddEditStudentView(StudentWrapper? student = null)
	{
		InitializeComponent();
		//DataContext = new AddEditStudentViewModel(student);

		AddEditStudentViewModel vm = new(student);
		DataContext = vm;

		// INFO - MVVM Zamykanie okna 3 - użycie delegata
		//if (vm.CloseAction == null)
		//{
		//	vm.CloseAction = new Action(() => this.Close());
		//}
		// inna wersja
		//vm.CloseAction = Close;

		// INFO - MVVM Zamykanie okna 4 - użycie eventa
		//vm.CloseWindowEvent += Close;
	}
}