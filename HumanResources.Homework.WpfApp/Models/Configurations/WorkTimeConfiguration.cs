using HumanResources.Homework.WpfApp.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResources.Homework.WpfApp.Models.Configurations;

public class WorkTimeConfiguration : IEntityTypeConfiguration<WorkTime>
{
	public void Configure(EntityTypeBuilder<WorkTime> builder)
	{
		builder
			.HasIndex(x => x.WorkTimeName)
			.IsUnique();

		builder
			.Property(x => x.WorkTimeName)
			.HasMaxLength(50)
			.IsRequired();
	}
}