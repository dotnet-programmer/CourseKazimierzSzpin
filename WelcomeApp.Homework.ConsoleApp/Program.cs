﻿Console.Write("Write your name: ");
string name = GetTextInput();

Console.Write("Write year of birth: ");
int year = GetYear();

Console.Write("Write month of birth: ");
int month = GetMonth();

Console.Write("Write day of birth: ");
int day = GetDay(year, month);

Console.Write("Write city of birth: ");
string birthplace = GetTextInput();

DateTime birthDate = new(year, month, day);
int age = (DateTime.Now.DayOfYear > birthDate.DayOfYear) ? (DateTime.Now.Year - year) : (DateTime.Now.Year - year - 1);

Console.WriteLine();
Console.WriteLine($"Hello {name}, you were born in {birthplace} and you are {age} years old.");
Console.ReadLine();

static string GetTextInput()
{
	string input;
	while (string.IsNullOrWhiteSpace(input = Console.ReadLine()))
	{
		ErrorMessage();
	}
	return input;
}

static int GetYear()
{
	int year;
	while (!int.TryParse(Console.ReadLine(), out year) || year < 1850 || year > DateTime.Now.Year)
	{
		ErrorMessage();
	}
	return year;
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
	=> Console.Write("Wrong input!");