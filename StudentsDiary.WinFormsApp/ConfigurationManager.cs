using StudentsDiary.WinFormsApp.Helpers;
using StudentsDiary.WinFormsApp.Models;
using StudentsDiary.WinFormsApp.Properties;

namespace StudentsDiary.WinFormsApp;

internal class ConfigurationManager
{
	private readonly FileHelper<Configuration> _configFile;
	private readonly Form _form;

	public ConfigurationManager(Form form)
	{
		_configFile = new(Program.DataFilePath, form.Name);
		_form = form;
	}

	public void LoadConfiguration()
	{
		if (Program.UseAppSettings)
		{
			SetWindowState(Settings.Default.IsMaximized, new Rectangle(Settings.Default.MainWindowLocation, Settings.Default.MainWindowSize));
		}
		else
		{
			Configuration config = _configFile.DeserializeFromJSON();
			SetWindowState(config.IsMaximized, config.WindowPosition);
		}
	}

	public void SaveConfiguration()
	{
		if (Program.UseAppSettings)
		{
			SaveWindowStateUsingAppSettings();
		}
		else
		{
			SaveWindowStateUsingConfigFile();
		}
	}

	private void SetWindowState(bool isMaximized, Rectangle windowPosition)
	{
		_form.WindowState = isMaximized ? FormWindowState.Maximized : FormWindowState.Normal;

		if (Screen.AllScreens.Any(screen => screen.WorkingArea.IntersectsWith(windowPosition)))
		{
			_form.StartPosition = FormStartPosition.Manual;
			_form.WindowState = FormWindowState.Normal;
			_form.DesktopBounds = windowPosition;
		}
		else
		{
			_form.StartPosition = FormStartPosition.CenterScreen;
		}
	}

	private void SaveWindowStateUsingAppSettings()
	{
		Settings.Default.IsMaximized = _form.WindowState == FormWindowState.Maximized;
		Settings.Default.MainWindowLocation = _form.DesktopBounds.Location;
		Settings.Default.MainWindowSize = _form.DesktopBounds.Size;
		Settings.Default.Save();
	}

	private void SaveWindowStateUsingConfigFile() => _configFile.SerializeToJSON(new Configuration()
	{
		IsMaximized = _form.WindowState == FormWindowState.Maximized,
		WindowPosition = _form.DesktopBounds
	});
}