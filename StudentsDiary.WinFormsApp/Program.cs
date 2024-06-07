namespace StudentsDiary.WinFormsApp;

internal static class Program
{
	public const string DataFileName = "StudentsDiaryData";
	public const bool UseAppSettings = false;

	public static readonly string DataFilePath = Path.Combine(Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%"), "StudentsDiary");

	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	private static void Main()
	{
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		Application.Run(new Main());
	}
}