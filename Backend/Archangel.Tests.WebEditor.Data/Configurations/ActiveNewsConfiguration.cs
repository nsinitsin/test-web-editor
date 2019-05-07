using Archangel.Tests.WebEditor.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archangel.Tests.WebEditor.Data.Configurations
{
    internal class ActiveNewsConfiguration : IEntityTypeConfiguration<ActiveNews>
    {
        public virtual void Configure(EntityTypeBuilder<ActiveNews> builder)
        {
            builder.HasBaseType<News>();
        }
    }
}