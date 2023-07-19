Console.WriteLine("The program checks the parity of numbers.");
Console.WriteLine("Type the numbers; the letter K will end the program.");

string input;
do
{
	Console.WriteLine();
	Console.Write("Write a number: ");
	input = Console.ReadLine().ToUpper();
	if (int.TryParse(input, out int number))
	{
		Console.WriteLine($"Number {number} is {((number % 2 == 0) ? "even" : "odd")}.");
	}
} while (input != "K");

Console.WriteLine("\nPress Enter to close.");
Console.ReadLine();