using Archangel.Tests.WebEditor.Data.Configurations;
using Archangel.Tests.WebEditor.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Archangel.Tests.WebEditor.Data
{
    public class WebEditorDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<News> News { get; set; }
        public DbSet<DraftNews> DraftNews { get; set; }
        public DbSet<ActiveNews> ActiveNews { get; set; }
        public DbSet<ArchivedNews> ArchivedNews { get; set; }
        
        public WebEditorDbContext(DbContextOptions<WebEditorDbContext> options, IConfiguration configuration) : base(options)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config["ConnectionStrings:WebEditorContext"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new DraftNewsConfiguration());
            modelBuilder.ApplyConfiguration(new ActiveNewsConfiguration());
            modelBuilder.ApplyConfiguration(new ArchivedNewsConfiguration());
        }
    }
}