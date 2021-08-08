using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrocTest.Models;

namespace PayrocTest.Data.Configuration
{
    public class LinkEntityConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url)
                .HasConversion(x => x.Value, x => Url.Create(x).Value);
        }
    }
}
