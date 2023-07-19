try
{
	Console.WriteLine("Podaj 1 liczbę:");
	var number1 = GetInput();

	Console.WriteLine("Jaką operacje chcesz wykonać? Możliwe operacje to: '+', '-', '*', '/'.");
	var action = Console.ReadLine();

	Console.WriteLine("Podaj 2 liczbę:");
	var number2 = GetInput();

	Console.WriteLine($"Wynik Twojego działania to: {Calculate(number1, number2, action)}");
}
catch (Exception ex)
{
	//logowanie do pliku
	Console.WriteLine(ex.Message);
}
finally
{
	Console.WriteLine("\nKoniec programu.");
	Console.ReadLine();
}

static int GetInput()
{
	if (!int.TryParse(Console.ReadLine(), out int input))
	{
		throw new Exception("Podana wartość jest nieprawidłowa.");
	}
	return input;
}

static int Calculate(int number1, int number2, string action) => action switch
{
	"+" => number1 + number2,
	"-" => number1 - number2,
	"*" => number1 * number2,
	"/" => number1 / number2,
	_ => throw new Exception("Wybrałeś złą operację!"),
};