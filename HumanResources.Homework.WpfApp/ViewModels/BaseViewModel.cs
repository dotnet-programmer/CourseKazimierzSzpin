﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HumanResources.Homework.WpfApp.ViewModels;

internal abstract class BaseViewModel : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}