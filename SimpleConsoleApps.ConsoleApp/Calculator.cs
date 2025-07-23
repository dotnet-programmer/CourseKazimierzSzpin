namespace SimpleConsoleApps.ConsoleApp;

internal class Calculator
{
	public static void Start()
	{
		Console.Write("Write 1st number: ");
		int number1 = GetInput();

		Console.Write("Write type of operation (+, -, *, /, %): ");
		string? action = Console.ReadLine();

		Console.Write("Write 2nd number: ");
		int number2 = GetInput();

		Console.WriteLine($"\n{number1} {action} {number2} = {Calculate(number1, number2, action)}\n");
	}

	private static int GetInput()
	{
		if (!int.TryParse(Console.ReadLine(), out int input))
		{
			throw new Exception("Wrong input!");
		}
		return input;
	}

	private static int Calculate(int number1, int number2, string? action)
		=> action switch
		{
			"+" => number1 + number2,
			"-" => number1 - number2,
			"*" => number1 * number2,
			"/" => number1 / number2,
			"%" => number1 % number2,
			_ => throw new Exception("Unknown operation!"),
		};
}