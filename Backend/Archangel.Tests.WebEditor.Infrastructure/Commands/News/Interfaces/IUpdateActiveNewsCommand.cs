using System.Threading.Tasks;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces
{
    public interface IUpdateActiveNewsCommand
    {
        Task<ICommandResult<long>> ExecuteAsync(long activeNewsId, string newtext);
    }
}