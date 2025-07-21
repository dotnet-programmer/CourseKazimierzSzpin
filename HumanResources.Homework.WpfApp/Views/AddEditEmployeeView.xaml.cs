using System.Windows;
using HumanResources.Homework.WpfApp.Models.Wrappers;
using HumanResources.Homework.WpfApp.ViewModels;

namespace HumanResources.Homework.WpfApp.Views;

public partial class AddEditEmployeeView : Window
{
	public AddEditEmployeeView(EmployeeWrapper employeeWrapper = null, bool fireTheEmployee = false)
	{
		InitializeComponent();
		DataContext = new AddEditEmployeeViewModel(employeeWrapper, fireTheEmployee);
	}
}