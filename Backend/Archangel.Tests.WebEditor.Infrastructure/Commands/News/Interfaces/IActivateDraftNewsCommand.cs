using System.Threading.Tasks;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces
{
    public interface IActivateDraftNewsCommand
    {
        Task<ICommandResult<bool>> ExecuteAsync(long draftId);
    }
}