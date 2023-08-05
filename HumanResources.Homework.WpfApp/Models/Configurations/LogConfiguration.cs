using HumanResources.Homework.WpfApp.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResources.Homework.WpfApp.Models.Configurations;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
	public void Configure(EntityTypeBuilder<Log> builder)
	{
		builder
			.Property(x => x.Logged)
			.IsRequired();

		builder
			.Property(x => x.User)
			.IsRequired();

		builder
			.Property(x => x.Exception)
			.IsRequired();
	}
}