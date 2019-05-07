using System.Threading.Tasks;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces
{
    public interface IGetLiveNewsCommand
    {
        Task<ICommandResult<Data.Domains.News>> ExecuteAsync();
    }
}