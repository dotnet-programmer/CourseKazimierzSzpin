try
{
	Console.Write("Write 1st number: ");
	var number1 = GetInput();

	Console.Write("Write type of operation (+, -, *, /, %): ");
	var action = Console.ReadLine();

	Console.Write("Write 2nd number: ");
	var number2 = GetInput();

	Console.WriteLine($"\n{number1} {action} {number2} = {Calculate(number1, number2, action)}\n");
}
catch (Exception ex)
{
	//log to file
	Console.WriteLine(ex.Message);
}
finally
{
	Console.WriteLine("\nPress any key to close.");
	Console.ReadLine();
}

static int GetInput()
{
	if (!int.TryParse(Console.ReadLine(), out int input))
	{
		throw new Exception("Wrong input!");
	}
	return input;
}

static int Calculate(int number1, int number2, string action) => action switch
{
	"+" => number1 + number2,
	"-" => number1 - number2,
	"*" => number1 * number2,
	"/" => number1 / number2,
	"%" => number1 % number2,
	_ => throw new Exception("Unknown operation!"),
};