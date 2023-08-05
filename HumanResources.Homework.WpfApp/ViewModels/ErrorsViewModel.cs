using System.Collections.ObjectModel;
using HumanResources.Homework.WpfApp.Models.Domains;
using HumanResources.Homework.WpfApp.Repositories;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal class ErrorsViewModel : BaseViewModel
{
	public ErrorsViewModel() => _logs = new(LogRepository.GetErrors());

	private ObservableCollection<Log> _logs;
	public ObservableCollection<Log> Logs
	{
		get => _logs;
		set { _logs = value; OnPropertyChanged(); }
	}
}