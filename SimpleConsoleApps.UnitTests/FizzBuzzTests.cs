using NUnit.Framework;
using SimpleConsoleApps.ConsoleApp;

namespace SimpleConsoleApps.UnitTests;

public class FizzBuzzTests
{
	//public void MetodaTestowana_Scenariusz_OczekiwaneZachowanie() { }

	[Test]
	public void CheckNumber_WhenInputIsDivisibleBy3AndBy5_ShouldReturnFizzBuzz()
	{
		// Arrange - przygotowanie obiektów
		var fizzBuzz = new FizzBuzzClass();

		// Act - działanie
		string result = fizzBuzz.CheckNumber(15);

		// Assert - sprawdzenie poprawności wyników
		Assert.That(result, Is.EqualTo("FizzBuzz"));
	}

	[Test]
	public void CheckNumber_WhenInputIsDivisibleOnlyBy3_ShouldReturnFizz()
	{
		// Arrange - przygotowanie obiektów
		var fizzBuzz = new FizzBuzzClass();

		// Act - działanie
		string result = fizzBuzz.CheckNumber(3);

		// Assert - sprawdzenie poprawności wyników
		Assert.That(result, Is.EqualTo("Fizz"));
	}

	[Test]
	public void CheckNumber_WhenInputIsDivisibleOnlyBy5_ShouldReturnBuzz()
	{
		// Arrange - przygotowanie obiektów
		var fizzBuzz = new FizzBuzzClass();

		// Act - działanie
		string result = fizzBuzz.CheckNumber(5);

		// Assert - sprawdzenie poprawności wyników
		Assert.That(result, Is.EqualTo("Buzz"));
	}

	[Test]
	public void CheckNumber_WhenInputIsNotDivisibleBy3OrBy5_ShouldReturnInput()
	{
		// Arrange - przygotowanie obiektów
		var fizzBuzz = new FizzBuzzClass();

		// Act - działanie
		string result = fizzBuzz.CheckNumber(16);

		// Assert - sprawdzenie poprawności wyników
		Assert.That(result, Is.EqualTo("16"));
	}
}