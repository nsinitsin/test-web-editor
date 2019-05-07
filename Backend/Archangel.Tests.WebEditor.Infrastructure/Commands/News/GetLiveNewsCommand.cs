using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News
{
    public class GetLiveNewsCommand : IGetLiveNewsCommand
    {
        private readonly IAsyncRepository<Data.Domains.News> _newsRepository;

        public GetLiveNewsCommand(IAsyncRepository<Data.Domains.News> newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<ICommandResult<Data.Domains.News>> ExecuteAsync()
        {
            var draftNews = (await _newsRepository.FindAsync(s=>s is ActiveNews)).ToList();
            if (draftNews.Count > 1)
                return new CommandResult<Data.Domains.News>("In database more then one draft", HttpStatusCode.InternalServerError);

            var singleDraftNews = draftNews.FirstOrDefault();
            if (singleDraftNews == null)
                return new CommandResult<Data.Domains.News>("System doesn't have any draft news.", HttpStatusCode.NotFound);

            return new CommandResult<Data.Domains.News>(singleDraftNews);
        }
    }
}