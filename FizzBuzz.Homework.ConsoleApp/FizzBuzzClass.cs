namespace FizzBuzz.Homework.ConsoleApp;

internal class FizzBuzzClass
{
	public string CheckNumber(int number)
	{
		if ((number % 3 == 0) && (number % 5 == 0))
		{
			return "FizzBuzz";
		}

		if (number % 3 == 0)
		{
			return "Fizz";
		}

		if (number % 5 == 0)
		{
			return "Buzz";
		}

		return number.ToString();
	}
}