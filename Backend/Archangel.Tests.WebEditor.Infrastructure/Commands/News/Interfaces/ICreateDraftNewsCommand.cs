using System.Threading.Tasks;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces
{
    public interface ICreateDraftNewsCommand
    {
        Task<ICommandResult<long>> ExecuteAsync(string inputModelText);
    }
}