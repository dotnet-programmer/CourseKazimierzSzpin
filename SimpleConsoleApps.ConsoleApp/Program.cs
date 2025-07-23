using SimpleConsoleApps.ConsoleApp;

Console.ForegroundColor = ConsoleColor.Green;

try
{
	while (true)
	{
		ShowMenu();
		DoWork();
		ShowEndInfo();
	}
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
	Console.WriteLine("\nPress any key to close.");
	Console.ReadKey();
}

static void ShowMenu()
{
	Console.Clear();
	Console.WriteLine("Avaliable projects:");
	Console.WriteLine("1. Calculator");
	Console.WriteLine("2. Welcome app - homework");
	Console.WriteLine("3. Even/odd - homework");
	Console.WriteLine("4. Random number game - homework");
	Console.WriteLine("5. Algorithm FizzBuzz - homework");
	Console.WriteLine("Any key to exit");
	Console.Write(">");
}

static void DoWork()
{
	var clickedKey = Console.ReadKey().KeyChar;
	Console.Clear();

	switch (clickedKey)
	{
		case '1':
			Calculator.Start();
			break;
		case '2':
			WelcomeApp.Start();
			break;
		case '3':
			EvenOrOdd.Start();
			break;
		case '4':
			RandomNumberGame.Start();
			break;
		case '5':
			FizzBuzz.Start();
			break;
		default:
			Environment.Exit(0);
			break;
	}
}

static void ShowEndInfo()
{
	Console.WriteLine();
	Console.WriteLine("Any key to return to the menu");
	Console.ReadKey();
}