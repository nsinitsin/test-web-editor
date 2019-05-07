using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Archangel.Tests.WebEditor.Data
{
    public class FakeDbContext
    {
        private readonly IConfiguration _config;
        public static bool IsAdded { get; set; }

        public FakeDbContext(IConfiguration config)
        {
            _config = config;
        }
        public WebEditorDbContext GetFakeDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebEditorDbContext>();
            optionsBuilder.UseInMemoryDatabase();
            var context = new WebEditorDbContext(optionsBuilder.Options, _config);
            if (IsAdded) return context;
            IsAdded = true;
            return context;
        }
    }
}