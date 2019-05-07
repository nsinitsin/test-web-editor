using Archangel.Tests.WebEditor.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archangel.Tests.WebEditor.Data.Configurations
{
    internal class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public virtual void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(d => d.NewsId);

            builder.Property(d => d.CreatedOn).IsRequired();
            builder.Property(d => d.Text).IsRequired();

            builder.ToTable("News");
        }
    }
}
