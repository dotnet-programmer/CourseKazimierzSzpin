﻿using System.Windows.Input;

namespace HumanResources.Homework.WpfApp.Commands;

internal class RelayCommandAsync(Func<object, Task> execute, Func<object, bool> canExecute = null) : ICommand
{
	private readonly Func<object, Task> _execute = execute;
	private readonly Func<object, bool> _canExecute = canExecute ?? (o => true);

	private long _isExecuting;

	public event EventHandler CanExecuteChanged
	{
		add => CommandManager.RequerySuggested += value;
		remove => CommandManager.RequerySuggested -= value;
	}

	public void RaiseCanExecuteChanged()
		=> CommandManager.InvalidateRequerySuggested();

	public bool CanExecute(object parameter)
	{
		if (Interlocked.Read(ref _isExecuting) != 0)
		{
			return false;
		}
		return _canExecute(parameter);
	}

	public async void Execute(object parameter)
	{
		Interlocked.Exchange(ref _isExecuting, 1);
		RaiseCanExecuteChanged();

		try
		{
			await _execute(parameter);
		}
		finally
		{
			Interlocked.Exchange(ref _isExecuting, 0);
			RaiseCanExecuteChanged();
		}
	}
}