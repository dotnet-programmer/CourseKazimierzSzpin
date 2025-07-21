using System.Windows;
using HumanResources.Homework.WpfApp.ViewModels;

namespace HumanResources.Homework.WpfApp.Views;

public partial class LoginView : Window
{
	public LoginView()
	{
		InitializeComponent();
		DataContext = new LoginViewModel();
	}
}