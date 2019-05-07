using Archangel.Tests.WebEditor.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archangel.Tests.WebEditor.Data.Configurations
{
    internal class ArchivedNewsConfiguration : IEntityTypeConfiguration<ArchivedNews>
    {
        public void Configure(EntityTypeBuilder<ArchivedNews> builder)
        {
            builder.HasBaseType<News>();

            builder.Property(d => d.ArchivedOn);
        }
    }
}