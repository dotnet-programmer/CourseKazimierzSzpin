namespace HumanResources.Homework.WpfApp.Models.Domains;

public class Employee
{
	public int EmployeeId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Position { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public decimal? Salary { get; set; }
	public DateTime HireDate { get; set; }
	public DateTime? FireDate { get; set; }
	public string Address { get; set; }
	public string Comments { get; set; }

	public int WorkTimeId { get; set; }
	public WorkTime WorkTime { get; set; }
}