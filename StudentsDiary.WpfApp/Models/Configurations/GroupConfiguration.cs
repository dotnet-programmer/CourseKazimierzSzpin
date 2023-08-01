using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsDiary.WpfApp.Models.Domains;

namespace StudentsDiary.WpfApp.Models.Configurations;

// INFO - EF Konfiguracja 2 - klasa konfiguracyjna - zalecane rozwiązanie
internal class GroupConfiguration : IEntityTypeConfiguration<Group>
{
	public void Configure(EntityTypeBuilder<Group> builder)
	{
		builder
			.HasIndex(x => x.Name)
			.IsUnique();

		builder
			.Property(x => x.Name)
			.HasMaxLength(20)
			.IsRequired();
	}
}