using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Data.Domains;
using Archangel.Tests.WebEditor.Data.Repositories;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;

namespace Archangel.Tests.WebEditor.Infrastructure.Commands.News
{
    public class GetDraftNewsCommand : IGetDraftNewsCommand
    {
        private readonly IAsyncRepository<Data.Domains.News> _draftNewsRepository;

        public GetDraftNewsCommand(IAsyncRepository<Data.Domains.News> draftNewsRepository)
        {
            _draftNewsRepository = draftNewsRepository;
        }

        public async Task<ICommandResult<DraftNews>> ExecuteAsync()
        {
            var draftNews = (await _draftNewsRepository.FindAsync(s => s is DraftNews)).ToList();
            if (draftNews.Count > 1)
                return new CommandResult<DraftNews>("In database more then one draft", HttpStatusCode.InternalServerError);

            var singleDraftNews = draftNews.FirstOrDefault();
            if (singleDraftNews == null)
                return new CommandResult<DraftNews>("System doesn't have any draft news.", HttpStatusCode.NotFound);

            return new CommandResult<DraftNews>(singleDraftNews as DraftNews);
        }
    }
}