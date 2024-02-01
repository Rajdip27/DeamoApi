using DemoApi.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApi.Infrastructure.EntityConfiguration;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
	public void Configure(EntityTypeBuilder<State> builder)
	{
		builder.ToTable(nameof(State));
		builder.HasKey(x => x.Id);
		builder.Property(x=> x.Name)
			.HasMaxLength(128)
			.IsRequired();
		builder.HasOne(x=>x.Country)
			.WithMany(x=>x.States)
			.HasForeignKey(x=>x.CountryId);
	}
}
