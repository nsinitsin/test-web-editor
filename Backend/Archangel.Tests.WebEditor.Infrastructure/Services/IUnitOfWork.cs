using System.Threading.Tasks;

namespace Archangel.Tests.WebEditor.Infrastructure.Services
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}