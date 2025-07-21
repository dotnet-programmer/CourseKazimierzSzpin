using System.Windows;
using HumanResources.Homework.WpfApp.ViewModels;

namespace HumanResources.Homework.WpfApp.Views;

public partial class ErrorsView : Window
{
	public ErrorsView()
	{
		InitializeComponent();
		DataContext = new ErrorsViewModel();
	}
}