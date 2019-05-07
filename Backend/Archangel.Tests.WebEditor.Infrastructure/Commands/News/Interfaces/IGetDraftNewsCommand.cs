using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces
{
    public interface IGetDraftNewsCommand
    {
        Task<ICommandResult<DraftNews>> ExecuteAsync();
    }
}