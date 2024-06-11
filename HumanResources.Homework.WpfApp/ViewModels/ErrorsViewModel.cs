﻿using System.Collections.ObjectModel;
using HumanResources.Homework.WpfApp.Models.Domains;
using HumanResources.Homework.WpfApp.Repositories;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal class ErrorsViewModel : BaseViewModel
{
	private readonly LogRepository _logRepository = new();

	public ErrorsViewModel()
		=> _logs = new(_logRepository.GetErrors());

	private ObservableCollection<Log> _logs;
	public ObservableCollection<Log> Logs
	{
		get => _logs;
		set { _logs = value; OnPropertyChanged(); }
	}
}