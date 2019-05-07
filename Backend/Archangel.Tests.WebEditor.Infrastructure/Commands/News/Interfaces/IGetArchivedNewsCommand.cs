using System.Collections.Generic;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces
{
    public interface IGetArchivedNewsCommand
    {
        Task<ICommandResult<IEnumerable<ArchivedNews>>> ExecuteAsync(int page, int take);
    }
}