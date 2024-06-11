using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsDiary.WpfApp.Models.Domains;

namespace StudentsDiary.WpfApp.Models.Configurations;

internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
	public void Configure(EntityTypeBuilder<Student> builder)
	{
		builder
			.Property(x => x.FirstName)
			.HasMaxLength(100)

			// INFO - EF Konfiguracja 4 - dodaje opis tabeli w polu "Description"
			//.HasComment("Imię")

			.IsRequired();

		builder
			.Property(x => x.LastName)
			.HasMaxLength(100)
			.IsRequired();

		builder
			.Property(x => x.Comments)
			.HasMaxLength(250)
			.IsRequired(false);

		builder
			.Property(x => x.Activities)
			.IsRequired(false);
	}
}