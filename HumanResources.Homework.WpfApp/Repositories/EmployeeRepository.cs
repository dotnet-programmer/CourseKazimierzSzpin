using HumanResources.Homework.WpfApp.Models;
using HumanResources.Homework.WpfApp.Models.Converters;
using HumanResources.Homework.WpfApp.Models.Domains;
using HumanResources.Homework.WpfApp.Models.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Homework.WpfApp.Repositories;

internal static class EmployeeRepository
{
	public static List<EmployeeWrapper> GetEmployees(int workTimeId, Employment employment)
	{
		using (AppDbContext context = new())
		{
			var employees = context.Employees.Include(x => x.WorkTime).AsQueryable();

			if (workTimeId != 0)
			{
				employees = employees.Where(x => x.WorkTimeId == workTimeId);
			}

			switch (employment)
			{
				case Employment.Zatrudnieni:
					employees = employees.Where(x => x.FireDate == null);
					break;
				case Employment.Zwolnieni:
					employees = employees.Where(x => x.FireDate != null);
					break;
				default:
					break;
			}

			return employees.ToList().Select(x => x.ToWrapper()).ToList();
		}
	}

	public static void AddEmployee(EmployeeWrapper employeeWrapper)
	{
		var employee = employeeWrapper.ToDao();

		using (AppDbContext context = new())
		{
			context.Employees.Add(employee);
			context.SaveChanges();
		}
	}

	public static async Task AddEmployeeAsync(EmployeeWrapper employeeWrapper)
	{
		var employee = employeeWrapper.ToDao();

		using (AppDbContext context = new())
		{
			context.Employees.Add(employee);
			await context.SaveChangesAsync();
		}
	}

	public static void UpdateEmployee(EmployeeWrapper employeeWrapper)
	{
		var employee = employeeWrapper.ToDao();

		using (AppDbContext context = new())
		{
			var employeeToUpdate = context.Employees.Find(employee.EmployeeId);
			UpdateEmployeeProperties(employeeToUpdate, employee);
			context.SaveChanges();
		}
	}

	public static async Task UpdateEmployeeAsync(EmployeeWrapper employeeWrapper)
	{
		var employee = employeeWrapper.ToDao();

		using (AppDbContext context = new())
		{
			var employeeToUpdate = await context.Employees.FindAsync(employee.EmployeeId);
			UpdateEmployeeProperties(employeeToUpdate, employee);
			await context.SaveChangesAsync();
		}
	}

	private static void UpdateEmployeeProperties(Employee employeeToUpdate, Employee employee)
	{
		employeeToUpdate.FirstName = employee.FirstName;
		employeeToUpdate.LastName = employee.LastName;
		employeeToUpdate.Position = employee.Position;
		employeeToUpdate.Email = employee.Email;
		employeeToUpdate.Phone = employee.Phone;
		employeeToUpdate.Salary = employee.Salary;
		employeeToUpdate.HireDate = employee.HireDate;
		employeeToUpdate.FireDate = employee.FireDate;
		employeeToUpdate.Address = employee.Address;
		employeeToUpdate.Comments = employee.Comments;
		employeeToUpdate.WorkTimeId = employee.WorkTimeId;
	}
}