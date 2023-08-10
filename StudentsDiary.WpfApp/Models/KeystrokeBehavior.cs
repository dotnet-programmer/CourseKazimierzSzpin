using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace StudentsDiary.WpfApp.Models;

// INFO - MVVM Zamykanie okna 5 - użycie Keystroke Behavior
internal class KeystrokeBehavior : Behavior<Window>
{
	public Key PressedKey { get; set; }

	protected override void OnAttached()
	{
		Window window = AssociatedObject;
		if (window != null)
		{
			window.KeyDown += Window_KeyDown;
		}
	}

	private void Window_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == PressedKey)
		{
			(sender as Window)?.Close();
		}
	}
}