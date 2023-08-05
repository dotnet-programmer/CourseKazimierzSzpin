using HumanResources.Homework.WpfApp.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResources.Homework.WpfApp.Models.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder
			.Property(x => x.FirstName)
			.HasMaxLength(50)
			.IsRequired();

		builder
			.Property(x => x.LastName)
			.HasMaxLength(70)
			.IsRequired();

		builder
			.Property(x => x.Position)
			.HasMaxLength(100);

		builder
			.Property(x => x.Email)
			.HasMaxLength(100);

		builder
			.Property(x => x.Phone)
			.HasMaxLength(20);

		builder
			.Property(x => x.Salary)
			.HasColumnType("money");

		builder
			.Property(x => x.Address)
			.HasMaxLength(200);

		builder
			.Property(x => x.Comments)
			.HasMaxLength(200);
	}
}