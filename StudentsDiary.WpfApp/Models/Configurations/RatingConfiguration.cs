using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsDiary.WpfApp.Models.Domains;

namespace StudentsDiary.WpfApp.Models.Configurations;

internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
	public void Configure(EntityTypeBuilder<Rating> builder)
		=> builder
			.Property(x => x.Rate)
			.IsRequired();
}