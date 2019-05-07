using System.Threading.Tasks;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces
{
    public interface IUpdateDraftNewsCommand
    {
        Task<ICommandResult<long>> ExecuteAsync(long draftId, string newtext);
    }
}