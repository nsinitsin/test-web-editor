using Archangel.Tests.WebEditor.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archangel.Tests.WebEditor.Data.Configurations
{
    internal class DraftNewsConfiguration : IEntityTypeConfiguration<DraftNews>
    {
        public virtual void Configure(EntityTypeBuilder<DraftNews> builder)
        {
            builder.HasBaseType<News>();
        }
    }
}