using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data;

namespace Archangel.Tests.WebEditor.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebEditorDbContext _context;

        public UnitOfWork(WebEditorDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}