int number = new Random().Next(101);
int iterations = 0;

Console.WriteLine("Random number game - try to find X from 0 to 100.");
Console.WriteLine();

while (true)
{
	Console.Write("Write a number from 0 to 100: ");

	if (int.TryParse(Console.ReadLine(), out int input) && input >= 0 && input <= 100)
	{
		iterations++;

		if (input > number)
		{
			Console.WriteLine($"{input} > X");
		}
		else if (input < number)
		{
			Console.WriteLine($"{input} < X");
		}
		else
		{
			Console.WriteLine();
			Console.Write($"You found the correct number - {number} in {iterations} steps.");
			Console.WriteLine();
			break;
		}
	}
}

Console.WriteLine("\nPress Enter to close.");
Console.ReadLine();