using System.Windows;
using HumanResources.Homework.WpfApp.ViewModels;

namespace HumanResources.Homework.WpfApp.Views;

/// <summary>
/// Interaction logic for LoginView.xaml
/// </summary>
public partial class LoginView : Window
{
	public LoginView()
	{
		InitializeComponent();
		DataContext = new LoginViewModel();
	}
}