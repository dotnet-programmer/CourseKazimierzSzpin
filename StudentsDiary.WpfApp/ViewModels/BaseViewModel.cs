using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentsDiary.WpfApp.ViewModels;

internal abstract class BaseViewModel : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}