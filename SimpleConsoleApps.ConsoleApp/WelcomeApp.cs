namespace SimpleConsoleApps.ConsoleApp;

internal class WelcomeApp
{
	public static void Start()
	{
		Console.Write("Write your name: ");
		string name = GetTextInput();

		Console.Write("\nWrite year of birth: ");
		int year = GetYear();

		Console.Write("\nWrite month of birth: ");
		int month = GetMonth();

		Console.Write("\nWrite day of birth: ");
		int day = GetDay(year, month);

		Console.Write("\nWrite city of birth: ");
		string birthplace = GetTextInput();

		DateTime birthDate = new(year, month, day);
		int age = (DateTime.Now.DayOfYear > birthDate.DayOfYear) ? (DateTime.Now.Year - year) : (DateTime.Now.Year - year - 1);

		Console.WriteLine();
		Console.WriteLine($"\nHello {name}, you were born in {birthplace} and you are {age} years old.");
	}

	static string GetTextInput()
	{
		string? input;
		while (string.IsNullOrWhiteSpace(input = Console.ReadLine()))
		{
			ErrorMessage();
		}
		return input;
	}

	static int GetYear()
	{
		while (true)
		{
			if ((!int.TryParse(Console.ReadLine(), out int year)) || year < 1850 || year > DateTime.Now.Year)
			{
				ErrorMessage();
				continue;
			}
			return year;
		}
	}

	static int GetMonth()
	{
		int month;
		while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
		{
			ErrorMessage();
		}
		return month;
	}

	static int GetDay(int year, int month)
	{
		int day;
		while (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > DateTime.DaysInMonth(year, month))
		{
			ErrorMessage();
		}
		return day;
	}

	static void ErrorMessage()
		=> Console.Write("Wrong input! Try again: ");
}