using System.Windows;
using HumanResources.Homework.WpfApp.ViewModels;

namespace HumanResources.Homework.WpfApp.Views;

public partial class MainView : Window
{
	public MainView()
	{
		InitializeComponent();
		DataContext = new MainViewModel();
	}
}