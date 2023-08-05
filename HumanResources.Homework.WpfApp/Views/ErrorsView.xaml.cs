using System.Windows;
using HumanResources.Homework.WpfApp.ViewModels;

namespace HumanResources.Homework.WpfApp.Views;

/// <summary>
/// Interaction logic for ErrorsView.xaml
/// </summary>
public partial class ErrorsView : Window
{
	public ErrorsView()
	{
		InitializeComponent();
		DataContext = new ErrorsViewModel();
	}
}