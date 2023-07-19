using FizzBuzz.Homework.ConsoleApp;

Console.WriteLine("The program checks if number is divisible by 3, 5 or 15.");
Console.WriteLine("Type the numbers; the letter K will end the program.");

FizzBuzzClass fizzBuzz = new();
string input;
do
{
	Console.WriteLine();
	Console.Write("Write a number: ");
	input = Console.ReadLine().ToUpper();
	if (int.TryParse(input, out int number))
	{
		Console.WriteLine($"{fizzBuzz.CheckNumber(number)}");
	}
} while (input != "K");

Console.WriteLine("\nPress Enter to close.");
Console.ReadLine();