using DemoApi.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApi.Infrastructure.EntityConfiguration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable(name: nameof(Country));
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Name).HasMaxLength(50).IsRequired(true);
    }
}
