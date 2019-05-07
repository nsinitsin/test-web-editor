using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News
{
    public class GetArchivedNewsCommand : IGetArchivedNewsCommand
    {
        private readonly IAsyncRepository<Data.Domains.News> _archivedRepository;

        public GetArchivedNewsCommand(IAsyncRepository<Data.Domains.News> archivedRepository)
        {
            _archivedRepository = archivedRepository;
        }

        public async Task<ICommandResult<IEnumerable<ArchivedNews>>> ExecuteAsync(int page, int take)
        {
            var archivedNews = await Task.FromResult(_archivedRepository.GetQuery(s=>s is ArchivedNews).Skip(page*take).Take(take).ToList());

            return new CommandResult<IEnumerable<ArchivedNews>>(archivedNews.Cast<ArchivedNews>());
        }
    }
}
