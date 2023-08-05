using HumanResources.Homework.WpfApp.Models.Domains;
using HumanResources.Homework.WpfApp.Models.Wrappers;

namespace HumanResources.Homework.WpfApp.Models.Converters;

public static class EmployeeConverter
{
	public static EmployeeWrapper ToWrapper(this Employee employee) => new()
	{
		EmployeeId = employee.EmployeeId,
		FirstName = employee.FirstName,
		LastName = employee.LastName,
		Position = employee.Position,
		Email = employee.Email,
		Phone = employee.Phone,
		Salary = employee.Salary,
		WorkTime = new WorkTimeWrapper { WorkTimeId = employee.WorkTime.WorkTimeId, WorkTimeName = employee.WorkTime.WorkTimeName },
		HireDate = employee.HireDate,
		FireDate = employee.FireDate,
		Address = employee.Address,
		Comments = employee.Comments,
	};

	public static Employee ToDao(this EmployeeWrapper employeeWrapper) => new()
	{
		EmployeeId = employeeWrapper.EmployeeId,
		FirstName = employeeWrapper.FirstName,
		LastName = employeeWrapper.LastName,
		Position = employeeWrapper.Position,
		Email = employeeWrapper.Email,
		Phone = employeeWrapper.Phone,
		Salary = employeeWrapper.Salary,
		WorkTimeId = employeeWrapper.WorkTime.WorkTimeId,
		HireDate = employeeWrapper.HireDate,
		FireDate = employeeWrapper.FireDate,
		Address = employeeWrapper.Address,
		Comments = employeeWrapper.Comments,
	};
}