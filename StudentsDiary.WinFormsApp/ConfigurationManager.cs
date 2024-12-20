﻿using StudentsDiary.WinFormsApp.FileSerialization;
using StudentsDiary.WinFormsApp.Models;
using StudentsDiary.WinFormsApp.Properties;

//using StudentsDiary.WinFormsApp.Helpers;

namespace StudentsDiary.WinFormsApp;

internal class ConfigurationManager(Form form)
{
	//private readonly FileHelper<Configuration> _configFile = new(Program.DataFilePath, form.Name);
	private readonly SerializeToFile<Configuration> _configFile = new SerializeToJson<Configuration>(Program.DataFilePath, form.Name);

	private readonly Form _form = form;

	public void LoadConfiguration()
	{
		if (Program.UseAppSettings)
		{
			SetWindowState(Settings.Default.IsMaximized, new Rectangle(Settings.Default.MainWindowLocation, Settings.Default.MainWindowSize));
		}
		else
		{
			//Configuration config = _configFile.DeserializeFromJSON();
			Configuration config = _configFile.Deserialize();
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
		if (isMaximized)
		{
			_form.WindowState = FormWindowState.Maximized;
		}
		else
		{
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
	}

	private void SaveWindowStateUsingAppSettings()
	{
		Settings.Default.IsMaximized = _form.WindowState == FormWindowState.Maximized;
		Settings.Default.MainWindowLocation = _form.DesktopBounds.Location;
		Settings.Default.MainWindowSize = _form.DesktopBounds.Size;
		Settings.Default.Save();
	}

	//private void SaveWindowStateUsingConfigFile()
	//=> _configFile.SerializeToJSON(new Configuration()
	//{
	//	IsMaximized = _form.WindowState == FormWindowState.Maximized,
	//	WindowPosition = _form.DesktopBounds
	//});
	private void SaveWindowStateUsingConfigFile()
		=> _configFile.Serialize(new Configuration()
		{
			IsMaximized = _form.WindowState == FormWindowState.Maximized,
			WindowPosition = _form.DesktopBounds
		});
}